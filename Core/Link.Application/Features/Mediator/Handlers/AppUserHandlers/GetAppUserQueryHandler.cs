using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Link.Application.Common;  // CustomResult sınıfını buradan ekleyin

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAppUserQueryHandler : IRequestHandler<GetAppUserQuery, CustomResult<List<GetAppUserQueryResult>>>
    {
        private readonly IRepository<AppUser> _repository;

        public GetAppUserQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<CustomResult<List<GetAppUserQueryResult>>> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            var result = values.Select(x => new GetAppUserQueryResult
            {
                Id = x.Id,
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
