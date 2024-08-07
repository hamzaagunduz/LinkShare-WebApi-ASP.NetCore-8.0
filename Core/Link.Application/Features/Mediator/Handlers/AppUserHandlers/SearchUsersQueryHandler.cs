using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Helper;
using Link.Application.Interfaces;
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
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, CustomResult<List<GetAppUserQueryResult>>>
    {
        private readonly IRepository<AppUser> _repository;

        public SearchUsersQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<CustomResult<List<GetAppUserQueryResult>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            // Verileri al
            var users = await _repository.GetAllAsync();

            // Merge Sort ile sıralama
            var sortedUsers = MergeSortHelper.MergeSort(users, user => user.FirstName);

            // Arama yap
            var searchQuery = request.Query.ToLower();
            var filteredUsers = sortedUsers.Where(user =>
                user.FirstName.ToLower().Contains(searchQuery) ||
                user.SurName.ToLower().Contains(searchQuery))
                .ToList();

            // Sonuçları dön
            var result = filteredUsers.Select(user => new GetAppUserQueryResult
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Email = user.Email,
                View = user.View,
                About = user.About,
                FollowersCount = user.FollowersCount,
                FollowingCount = user.FollowingCount,
                ImageUrl = user.ImageUrl,
                Password = user.Password,
                PostCount = user.PostCount,
                SurName = user.SurName
            }).ToList();

            return new CustomResult<List<GetAppUserQueryResult>>(result, HttpStatusCode.OK);
        }
    }
}
