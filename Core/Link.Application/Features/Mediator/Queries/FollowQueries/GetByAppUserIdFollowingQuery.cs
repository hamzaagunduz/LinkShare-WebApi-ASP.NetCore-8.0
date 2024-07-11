using Link.Application.Common;
using Link.Application.Features.Mediator.Results.FollowResults;
using Link.Application.Features.Mediator.Results.LinkResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.FollowQueries
{
    public class GetByAppUserIdFollowingQuery : IRequest<CustomResult<List<GetByAppUserIdFollowingQueryResult>>>
    {
        public GetByAppUserIdFollowingQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
