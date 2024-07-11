using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
    {
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Following> _followingRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnfollowUserCommandHandler(
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

        public async Task Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var followerUser = await _userManager.FindByIdAsync(userIdClaim.Value.ToString());
            var followingUser = await _userManager.FindByIdAsync(request.FollowingUserId.ToString());

            var following = await _followingRepository
                .GetByConditionAsync(f => f.AppUserID == int.Parse(userIdClaim.Value) && f.UserName == followingUser.UserName);


            if (following != null)
            {
                await _followingRepository.RemoveAsync(following);
            }

            var follower = await _followerRepository
         .GetByConditionAsync(f => f.AppUserID == request.FollowingUserId && f.UserName == followerUser.UserName);
            if (follower != null)
            {
                await _followerRepository.RemoveAsync(follower);
            }

            followerUser.FollowingCount--;
            followingUser.FollowersCount--;

            await _userManager.UpdateAsync(followerUser);
            await _userManager.UpdateAsync(followingUser);

        }
    }
}
