using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.FollowQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
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
    public class GetByAppUserIdFollowersQueryHandler : IRequestHandler<GetByAppUserIdFollowersQuery,CustomResult< List<GetByAppUserIdFollowersQueryResult>>>
    {
        private readonly IFollowRepository _followRepository;

        public GetByAppUserIdFollowersQueryHandler(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<CustomResult<List<GetByAppUserIdFollowersQueryResult>>> Handle(GetByAppUserIdFollowersQuery request, CancellationToken cancellationToken)
        {
            var followers = await _followRepository.GetFollowersAsync(request.UserId);

            var result = followers.Select(f => new GetByAppUserIdFollowersQueryResult
            {
                UserName = f.UserName,
                Name = f.Name,

            }).ToList();

            return new CustomResult<List<GetByAppUserIdFollowersQueryResult>>(result, HttpStatusCode.OK);


        }

        //public async Task<List<GetByAppUserIdFollowersQueryResult>> Handle(GetByAppUserIdFollowersQuery request, CancellationToken cancellationToken)
        //{
        //    var followers = await _followRepository.GetFollowersAsync(request.UserId);

        //    var result = followers.Select(f => new GetByAppUserIdFollowersQueryResult
        //    {
        //        //AppUserID = f.AppUserID,
        //        UserName = f.UserName,
        //        Name = f.Name,
        //        //AppUserFollowerID = f.AppUserFollowerID,
        //        //FollowerID=f.FollowerID,
        //    }).ToList();

        //    return result;
        //}
    }
}
