using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, CustomResult<string>>
{
    private readonly IRepository<Follower> _followerRepository;
    private readonly IRepository<Following> _followingRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateFollowCommandHandler(
        IRepository<Follower> followerRepository,
        IRepository<Following> followingRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _followerRepository = followerRepository;
        _followingRepository = followingRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CustomResult<string>> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        var LoginUserId = int.Parse(userIdClaim.Value);

        if (userIdClaim == null)
        {
            var error = "User ID claim Alanı Tokende Bulunamadı Unauthorized.";
            return new CustomResult<string>(null, HttpStatusCode.Unauthorized, new List<string> { error });
        }

        var followerUser = await _userManager.FindByIdAsync(userIdClaim.Value);
        if (followerUser == null)
        {
            throw new ArgumentNullException($"User with ID '{userIdClaim.Value}' not found.");
        }

        var followingUser = await _userManager.FindByIdAsync(request.FollowingUserId.ToString());
        if (followingUser == null)
        {
            throw new ArgumentNullException($"User with ID '{request.FollowingUserId}' not found.");
        }

        // Check if the user is already following the target user
        var existingFollowing = await _followingRepository
            .GetByConditionAsync(f => f.AppUserID == int.Parse(userIdClaim.Value) && f.AppUserFollowingID == request.FollowingUserId);

        if (LoginUserId != request.FollowingUserId)
        {
            if (existingFollowing != null)
            {
                // User is already following, so unfollow
                await _followingRepository.RemoveAsync(existingFollowing);

                var existingFollower = await _followerRepository
                    .GetByConditionAsync(f => f.AppUserID == request.FollowingUserId && f.AppUserFollowerID == int.Parse(userIdClaim.Value));

                if (existingFollower != null)
                {
                    await _followerRepository.RemoveAsync(existingFollower);
                }

                // Update following and follower counts
                followerUser.FollowingCount--;
                followingUser.FollowersCount--;

                await _userManager.UpdateAsync(followerUser);
                await _userManager.UpdateAsync(followingUser);

                return new CustomResult<string>("Takipten Çıkma Başarılı.", HttpStatusCode.OK);
            }
            else
            {

                // User is not following, so follow
                var following = new Following
                {
                    AppUserID = int.Parse(userIdClaim.Value),
                    AppUserFollowingID = request.FollowingUserId,
                    UserName = followingUser.UserName,
                    Name = $"{followingUser.FirstName} {followingUser.SurName}"
                };

                await _followingRepository.CreateAsync(following);

                var follower = new Follower
                {
                    AppUserID = request.FollowingUserId,
                    AppUserFollowerID = int.Parse(userIdClaim.Value),
                    UserName = followerUser.UserName,
                    Name = $"{followerUser.FirstName} {followerUser.SurName}"
                };

                await _followerRepository.CreateAsync(follower);

                // Update following and follower counts
                followerUser.FollowingCount++;
                followingUser.FollowersCount++;

                await _userManager.UpdateAsync(followerUser);
                await _userManager.UpdateAsync(followingUser);

                return new CustomResult<string>("Takip Oluşturma Başarılı.", HttpStatusCode.OK);
            }
        }
        else
        {
            return new CustomResult<string>("Kendini Takip edemezsin", HttpStatusCode.OK);

        }
    }
}
