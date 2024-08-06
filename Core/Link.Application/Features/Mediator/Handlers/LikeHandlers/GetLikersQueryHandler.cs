using Link.Application.Features.Mediator.Commands.LikeCommands;
using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using Link.Application.Interfaces.LinkInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LikeHandlers
{
    public class GetLikersQueryHandler : IRequestHandler<GetLikersQuery, List<GetLikersQueryResult>>
    {
        private readonly ICommentRepository _likeRepository;

        public GetLikersQueryHandler(ICommentRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<List<GetLikersQueryResult>> Handle(GetLikersQuery request, CancellationToken cancellationToken)
        {
            if (request.EntityType == EntityType.Comment)
            {
                return await _likeRepository.GetLikersForCommentAsync(request.EntityId);
            }
            else if (request.EntityType == EntityType.Answer)
            {
                return await _likeRepository.GetLikersForAnswerAsync(request.EntityId);
            }
            else
            {
                // Invalid EntityType
                return new List<GetLikersQueryResult>();
            }
        }
    }
}
