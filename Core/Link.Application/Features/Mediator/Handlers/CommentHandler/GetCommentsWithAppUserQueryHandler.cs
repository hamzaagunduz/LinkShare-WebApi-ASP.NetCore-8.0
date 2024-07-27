using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.CommentQueries;
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
    public class GetCommentsWithAppUserQueryHandler : IRequestHandler<GetCommentsWithAppUserQuery, CustomResult<List<GetCommentsWithAppUserQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsWithAppUserQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<CustomResult<List<GetCommentsWithAppUserQueryResult>>> Handle(GetCommentsWithAppUserQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsWithAppUserByUserIDAsync(request.id);

            var result = comments.Select(c => new GetCommentsWithAppUserQueryResult
            {
                Comment=c.Comment,
                ProfileCommentID = c.ProfileCommentID,
                Like = c.Like,
                View=c.View,
                FirstName = c.FirstName,
                SurName = c.SurName,
                UserName= c.UserName,
            }).ToList();

            return new CustomResult<List<GetCommentsWithAppUserQueryResult>>(result, HttpStatusCode.OK);
        }
    }
}
