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
    public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand>
    {
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Following> _followingRepository;
        private readonly UserManager<AppUser> _userManager;

        public CreateFollowCommandHandler(IRepository<Follower> followerRepository, IRepository<Following> followingRepository, UserManager<AppUser> userManager)
        {
            _followerRepository = followerRepository;
            _followingRepository = followingRepository;
            _userManager = userManager;
        }

        public async Task Handle(CreateFollowCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _userManager.FindByIdAsync(request.FollowerUserId.ToString());
            var followingUser = await _userManager.FindByIdAsync(request.FollowingUserId.ToString());

            var following = new Following
            {
                AppUserID = request.FollowerUserId,
                AppUserFollowingID= request.FollowingUserId,
                UserName = followingUser.UserName,
                Name = followingUser.FirstName + " " + followingUser.SurName
            };

            await _followingRepository.CreateAsync(following);

            // Follower tablosuna ekleme
            var follower = new Follower
            {  
                AppUserID = request.FollowingUserId,
                AppUserFollowerID= request.FollowerUserId,
                UserName = followerUser.UserName,
                Name = followerUser.FirstName + " " + followerUser.SurName
            };

            await _followerRepository.CreateAsync(follower);

            // Following ve Follower sayısını güncelleme
            followerUser.FollowingCount++;
            followingUser.FollowersCount++;

            await _userManager.UpdateAsync(followerUser);
            await _userManager.UpdateAsync(followingUser);

        }
    }
}
