using Link.Application.Features.Mediator.Commands.LikeCommands;
using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LikeHandlers
{
    public class GetLikersInfQueryHandler : IRequestHandler<GetLikersInfQuery, List<GetLikersQueryResult>>
    {
        private readonly ICommentRepository _likeRepository;

        public GetLikersInfQueryHandler(ICommentRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<List<GetLikersQueryResult>> Handle(GetLikersInfQuery request, CancellationToken cancellationToken)
        {
            return await _likeRepository.GetLikersAsync(request.EntityId, request.EntityType, request.Page, request.PageSize);
        }
    }
}
