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

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, CustomResult<Linke>>
    {
        private readonly IRepository<Linke> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLinkCommandHandler(IRepository<Linke> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomResult<Linke>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("User ID claim not found in token.");
                }

                var newLink = new Linke
                {
                    AppUserID = int.Parse(userIdClaim.Value),
                    LinkName = request.LinkName,
                    LinkUrl = request.LinkUrl,
                };



                // Save the new link to the repository
                await _repository.CreateAsync(newLink);

                return new CustomResult<Linke>(newLink, System.Net.HttpStatusCode.OK);
            }



            catch (UnauthorizedAccessException ex)
            {
                var errors = new List<string> { ex.Message };
                return new CustomResult<Linke>(null, System.Net.HttpStatusCode.Unauthorized, errors);
            }
            catch (ValidationException ex)
            {
                var errors = new List<string> { ex.Message };
                return new CustomResult<Linke>(null, System.Net.HttpStatusCode.BadRequest, errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return new CustomResult<Linke>(null, System.Net.HttpStatusCode.InternalServerError, errors);
            }
        }
    }
}
