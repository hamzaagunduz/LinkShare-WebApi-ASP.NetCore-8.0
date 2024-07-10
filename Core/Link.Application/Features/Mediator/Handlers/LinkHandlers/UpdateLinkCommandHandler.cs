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
    public class UpdateLinkCommandHandler : IRequestHandler<UpdateLinkCommand>
    {
        private readonly IRepository<Linke> _repository;

        public UpdateLinkCommandHandler(IRepository<Linke> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.LinkeID);
            values.LinkName = request.LinkName;
            values.LinkUrl = request.LinkUrl;

            await _repository.UpdateAsync(values);

        }
    }
}