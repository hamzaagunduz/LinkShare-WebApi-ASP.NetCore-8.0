using Link.Application.Features.Mediator.Queries.CommentQueries;
using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class GetByAppUserIDCommentQueryHandler : IRequestHandler<GetByAppUserIDCommentQuery, List<GetByAppUserIDCommentQueryResult>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetByAppUserIDCommentQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<GetByAppUserIDCommentQueryResult>> Handle(GetByAppUserIDCommentQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsByAppUserIDAsync(request.id);

            return comments.Select(c => new GetByAppUserIDCommentQueryResult
            {
                WriterID = c.WriterID,
                Comment = c.Comment,
                View = c.View,
                Like = c.Like,
                Hidden = c.Hidden,
                Time = c.Time
            }).ToList();
        }
    }
}
