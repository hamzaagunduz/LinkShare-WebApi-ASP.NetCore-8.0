using Link.Application.Features.Mediator.Results.FollowResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.FollowQueries
{
    public class GetByAppUserIdFollowersQuery:IRequest<List<GetByAppUserIdFollowersQueryResult>>
    {
        public GetByAppUserIdFollowersQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
