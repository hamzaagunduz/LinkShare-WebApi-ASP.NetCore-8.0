using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Interfaces;
using Link.Application.Interfaces.FollowInterfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetRandomUsersQueryHandler : IRequestHandler<GetRandomUsersQuery, CustomResult<List<GetAppUserQueryResult>>>
    {
        private readonly IFollowRepository _followRepository;

        public GetRandomUsersQueryHandler(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<CustomResult<List<GetAppUserQueryResult>>> Handle(GetRandomUsersQuery request, CancellationToken cancellationToken)
        {
            var values = await _followRepository.GetRandomUsers(request.id);

            var result = values.Select(x => new GetAppUserQueryResult
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                Email = x.Email,
                View = x.View,
                About = x.About,
                FollowersCount = x.FollowersCount,
                FollowingCount = x.FollowingCount,
                ImageUrl = x.ImageUrl,
                Password = x.Password,
                PostCount = x.PostCount,
                SurName = x.SurName
            }).ToList();

            return new CustomResult<List<GetAppUserQueryResult>>(result, HttpStatusCode.OK);
        }
    }
}
