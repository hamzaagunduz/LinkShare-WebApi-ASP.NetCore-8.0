using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, CustomResult<Follower>>
    {
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Following> _followingRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateFollowCommandHandler(IRepository<Follower> followerRepository, IRepository<Following> followingRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _followerRepository = followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
            _followingRepository = followingRepository ?? throw new ArgumentNullException(nameof(followingRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<CustomResult<Follower>> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("User ID claim not found in token.");
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

                return new CustomResult<Follower>(null, HttpStatusCode.OK);
            }
            catch (UnauthorizedAccessException ex)
            {
                return new CustomResult<Follower>(null, HttpStatusCode.Unauthorized);

            }
            catch (ArgumentNullException ex)
            {
                return new CustomResult<Follower>(null, HttpStatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return new CustomResult<Follower>(null, HttpStatusCode.InternalServerError);

            }
        }
    }
}
