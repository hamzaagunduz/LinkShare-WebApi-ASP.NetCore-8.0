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

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand>
    {
        private readonly IRepository<Linke> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<Linke> _validator;

        public CreateLinkCommandHandler(IRepository<Linke> repository, IHttpContextAccessor httpContextAccessor, IValidator<Linke> validator)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }

        public async Task Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                // Handle the case where the user ID claim is not found
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }

            var newLink = new Linke
            {
                AppUserID = int.Parse(userIdClaim.Value),
                LinkName = request.LinkName,
                LinkUrl = request.LinkUrl,
            };

            ValidationResult result = await _validator.ValidateAsync(newLink, cancellationToken);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            await _repository.CreateAsync(newLink);
        }
    }
}
