using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.FollowQueries
{

    public class CheckFollowStatusQuery : IRequest<bool>
    {
        public int UserId { get; }
        public int FollowingUserId { get; }

        public CheckFollowStatusQuery(int userId, int followingUserId)
        {
            UserId = userId;
            FollowingUserId = followingUserId;
        }
    }
}
