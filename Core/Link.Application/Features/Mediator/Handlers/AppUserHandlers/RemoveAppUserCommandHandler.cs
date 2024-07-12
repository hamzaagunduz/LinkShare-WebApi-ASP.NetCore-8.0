using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
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
    public class RemoveAppUserCommandHandler : IRequestHandler<RemoveAppUserCommand, CustomResult<AppUser>>
    {
        private readonly IRepository<AppUser> _repository;

        public RemoveAppUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        public async Task<CustomResult<AppUser>> Handle(RemoveAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new CustomResult<AppUser>(null, HttpStatusCode.NotFound);
            }

            try
            {
                await _repository.RemoveAsync(user);
                return new CustomResult<AppUser>(user, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Handle exception, log the error, etc.
                return new CustomResult<AppUser>(null, HttpStatusCode.InternalServerError);
            }

        }
    }
}
