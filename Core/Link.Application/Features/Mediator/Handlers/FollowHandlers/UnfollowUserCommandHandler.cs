using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
    {
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Following> _followingRepository;
        private readonly UserManager<AppUser> _userManager;

        public UnfollowUserCommandHandler(
            IRepository<Follower> followerRepository,
            IRepository<Following> followingRepository,
            UserManager<AppUser> userManager)
        {
            _followerRepository = followerRepository;
            _followingRepository = followingRepository;
            _userManager = userManager;
        }

        public async Task Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _userManager.FindByIdAsync(request.FollowerUserId.ToString());
            var followingUser = await _userManager.FindByIdAsync(request.FollowingUserId.ToString());

            var following = await _followingRepository
                .GetByConditionAsync(f => f.AppUserID == request.FollowerUserId && f.UserName == followingUser.UserName);
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
