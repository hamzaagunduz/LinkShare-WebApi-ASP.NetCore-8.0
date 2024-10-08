﻿using Link.Application.Features.Mediator.Commands.LinkCommands;
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
    public class RemoveLinkCommandHandler : IRequestHandler<RemoveLinkCommand>
    {
        private readonly IRepository<Linke> _repository;

        public RemoveLinkCommandHandler(IRepository<Linke> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveLinkCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.id);
            await _repository.RemoveAsync(value);
        }
    }
}