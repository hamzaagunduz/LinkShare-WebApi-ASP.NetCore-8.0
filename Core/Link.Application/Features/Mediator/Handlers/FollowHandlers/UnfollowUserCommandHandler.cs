using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, CustomResult<string>>
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

        public async Task<CustomResult<string>> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
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

            var following = await _followingRepository
                    .GetByConditionAsync(f => f.AppUserID == int.Parse(userIdClaim.Value) && f.AppUserFollowingID == request.FollowingUserId);



                if (following != null)
                {
                    await _followingRepository.RemoveAsync(following);
                }

                var follower = await _followerRepository
                    .GetByConditionAsync(f => f.AppUserID == request.FollowingUserId && f.AppUserFollowerID == int.Parse(userIdClaim.Value));

                if (follower != null)
                {
                    await _followerRepository.RemoveAsync(follower);
                }

                // Update following and follower counts
                followerUser.FollowingCount--;
                followingUser.FollowersCount--;

                await _userManager.UpdateAsync(followerUser);
                await _userManager.UpdateAsync(followingUser);

                return new CustomResult<string>(null, HttpStatusCode.OK);

            }


        }
    }

