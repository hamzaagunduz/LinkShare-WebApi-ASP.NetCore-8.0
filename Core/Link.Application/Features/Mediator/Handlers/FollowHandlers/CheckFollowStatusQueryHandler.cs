using Link.Application.Features.Mediator.Queries.FollowQueries;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class CheckFollowStatusQueryHandler : IRequestHandler<CheckFollowStatusQuery, bool>
    {
        private readonly IRepository<Following> _followingRepository;

        public CheckFollowStatusQueryHandler(IRepository<Following> followingRepository)
        {
            _followingRepository = followingRepository;
        }

        public async Task<bool> Handle(CheckFollowStatusQuery request, CancellationToken cancellationToken)
        {
            var isFollowing = await _followingRepository
                .GetByConditionAsync(f => f.AppUserID == request.UserId && f.AppUserFollowingID == request.FollowingUserId);

            return isFollowing != null;
        }
    }
}
