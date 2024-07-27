using Link.Application.Features.Mediator.Commands.CommentCommands;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand>
    {
        private readonly IRepository<ProfileComment> _repository;

        public RemoveCommentCommandHandler(IRepository<ProfileComment> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.id);
            await _repository.RemoveAsync(value);
        }
    }
}
