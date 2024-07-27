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

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, CustomResult<string>>
    {
        private readonly IRepository<Linke> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLinkCommandHandler(IRepository<Linke> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomResult<string>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {

                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    return new CustomResult<string>("User ID claim Alanı Tokende Bulunamadı Unauthorized.", HttpStatusCode.Unauthorized);

                }

            var newLink = new Linke
                {
                    AppUserID = int.Parse(userIdClaim.Value),
                    LinkName = request.LinkName,
                    LinkUrl = request.LinkUrl,
                };



                // Save the new link to the repository
                await _repository.CreateAsync(newLink);

                return new CustomResult<string>("Link Oluşturma successfully.", HttpStatusCode.OK);
            }




    }
}
