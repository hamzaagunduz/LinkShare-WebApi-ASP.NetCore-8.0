using Link.Application.Features.Mediator.Commands.LinkCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Link.Application.Common;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, CustomResult<string>>
    {
        private readonly IRepository<Linke> _repository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLinkCommandHandler(IRepository<Linke> repository, IHttpContextAccessor httpContextAccessor, IRepository<AppUser> repositoryUser)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = repositoryUser;
        }

        public async Task<CustomResult<string>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {

                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    return new CustomResult<string>("User ID claim Alanı Tokende Bulunamadı Unauthorized.", HttpStatusCode.Unauthorized);

                }

            var userId = int.Parse(userIdClaim.Value);
            var user = await _userRepository.GetByIdAsync(userId);
            var now = DateTime.UtcNow;

            if (user.LastLinkAddedTime.HasValue && (now - user.LastLinkAddedTime.Value).TotalSeconds < 3)

            {
                return new CustomResult<string>("Lütfen tekrar yorum eklemeden önce 2 saniye bekleyin.", HttpStatusCode.BadRequest);
            }
            else {
                var newLink = new Linke
                {
                    AppUserID = int.Parse(userIdClaim.Value),
                    LinkName = request.LinkName,
                    LinkUrl = request.LinkUrl,
                };


                await _repository.CreateAsync(newLink);
                user.LastLinkAddedTime = now;
                await _userRepository.UpdateAsync(user);
            }

            return new CustomResult<string>("Link Oluşturma successfully.", HttpStatusCode.OK);
            }




    }
}
