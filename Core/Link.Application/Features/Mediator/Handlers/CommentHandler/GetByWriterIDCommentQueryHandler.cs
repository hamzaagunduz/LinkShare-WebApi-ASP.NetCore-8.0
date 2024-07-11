using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.CommentQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class GetByWriterIDCommentQueryHandler : IRequestHandler<GetByWriterIDCommentQuery, CustomResult< List<GetByWriterIDCommentQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetByWriterIDCommentQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CustomResult<List<GetByWriterIDCommentQueryResult>>> Handle(GetByWriterIDCommentQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsByAppUserIDAsync(request.id);

            var result = comments.Select(c => new GetByWriterIDCommentQueryResult
            {
                AppUserID = c.AppUserID,
                Comment = c.Comment,
                View = c.View,
                Like = c.Like,
                Hidden = c.Hidden,
                Time = c.Time
            }).ToList();
            return new CustomResult<List<GetByWriterIDCommentQueryResult>>(result, HttpStatusCode.OK);

        }
        //public async Task<List<GetByWriterIDCommentQueryResult>> Handle(GetByWriterIDCommentQuery request, CancellationToken cancellationToken)
        //{
        //    var comments = await _commentRepository.GetCommentsByAppUserIDAsync(request.id);

        //    return comments.Select(c => new GetByWriterIDCommentQueryResult
        //    {
        //        AppUserID = c.AppUserID,
        //        Comment = c.Comment,
        //        View = c.View,
        //        Like = c.Like,
        //        Hidden = c.Hidden,
        //        Time = c.Time
        //    }).ToList();
        //}
    }
}
