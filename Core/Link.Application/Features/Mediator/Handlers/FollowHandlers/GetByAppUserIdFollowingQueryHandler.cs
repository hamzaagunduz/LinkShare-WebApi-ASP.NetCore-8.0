using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.FollowQueries;
using Link.Application.Features.Mediator.Results.FollowResults;
using Link.Application.Interfaces.FollowInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.FollowHandlers
{
    public class GetByAppUserIdFollowingQueryHandler : IRequestHandler<GetByAppUserIdFollowingQuery, CustomResult< List<GetByAppUserIdFollowingQueryResult>>>
    {
        private readonly IFollowRepository _followRepository;

        public GetByAppUserIdFollowingQueryHandler(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<CustomResult<List<GetByAppUserIdFollowingQueryResult>>> Handle(GetByAppUserIdFollowingQuery request, CancellationToken cancellationToken)
        {
            var followings = await _followRepository.GetFollowingAsync(request.UserId);

            var result = followings.Select(f => new GetByAppUserIdFollowingQueryResult
            {
                UserName = f.UserName,
                Name = f.Name,
            }).ToList();

            return new CustomResult<List<GetByAppUserIdFollowingQueryResult>>(result, HttpStatusCode.OK);
        }

        //public async Task<List<GetByAppUserIdFollowingQueryResult>> Handle(GetByAppUserIdFollowingQuery request, CancellationToken cancellationToken)
        //{
        //    var followings = await _followRepository.GetFollowingAsync(request.UserId);

        //    var result = followings.Select(f => new GetByAppUserIdFollowingQueryResult
        //    {
        //        UserName = f.UserName,
        //        Name = f.Name,
        //    }).ToList();

        //    return result;
        //}
    }
}
