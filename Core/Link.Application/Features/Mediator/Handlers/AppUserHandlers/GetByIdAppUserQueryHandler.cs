using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetByIdAppUserQueryHandler : IRequestHandler<GetByIdAppUserQuery, CustomResult<GetByIdAppUserQueryResult>>
    {
        private readonly IRepository<AppUser> _repository;

        public GetByIdAppUserQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<CustomResult<GetByIdAppUserQueryResult>> Handle(GetByIdAppUserQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.id);

            var result = new GetByIdAppUserQueryResult
            {
                Id = value.Id,
                FirstName = value.FirstName,
                Email = value.Email,
                View = value.View,
                About = value.About,
                FollowersCount = value.FollowersCount,
                FollowingCount = value.FollowingCount,
                ImageUrl = value.ImageUrl,
                Password = value.Password,
                PostCount = value.PostCount,
                SurName = value.SurName,
                UserName = value.UserName,
                LastLinkAddedTime=value.LastLinkAddedTime,
            };

            return new CustomResult<GetByIdAppUserQueryResult>(result, System.Net.HttpStatusCode.OK);
        }


        //public async Task<GetByIdAppUserQueryResult> Handle(GetByIdAppUserQuery request, CancellationToken cancellationToken)
        //{
        //    var values = await _repository.GetByIdAsync(request.id);

        //    return new GetByIdAppUserQueryResult
        //    {
        //        Id = values.Id,
        //        FirstName = values.FirstName,
        //        Email = values.Email,
        //        View = values.View,
        //        About = values.About,
        //        FollowersCount = values.FollowersCount,
        //        FollowingCount = values.FollowingCount,
        //        ImageUrl = values.ImageUrl,
        //        Password = values.Password,
        //        PostCount = values.PostCount,
        //        SurName = values.SurName,
        //        UserName = values.UserName
        //    };
        //}


    }
}

