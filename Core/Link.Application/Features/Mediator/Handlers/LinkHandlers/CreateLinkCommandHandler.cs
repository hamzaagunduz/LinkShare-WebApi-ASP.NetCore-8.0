using Link.Application.Features.Mediator.Commands.LinkCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand>
    {
        private readonly IRepository<Linke> _repository;

        public CreateLinkCommandHandler(IRepository<Linke> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var newLink = new Linke
            {
                AppUserID = request.AppUserID,
                LinkName= request.LinkName,
                LinkUrl= request.LinkUrl,
            };
            await _repository.CreateAsync(newLink);
        }
    }
}
