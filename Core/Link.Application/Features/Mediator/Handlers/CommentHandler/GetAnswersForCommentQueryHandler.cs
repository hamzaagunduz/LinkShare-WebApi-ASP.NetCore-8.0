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
    public class GetAnswersForCommentQueryHandler : IRequestHandler<GetAnswersForCommentQuery, CustomResult<List<GetAnswersForCommentQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAnswersForCommentQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CustomResult<List<GetAnswersForCommentQueryResult>>> Handle(GetAnswersForCommentQuery request, CancellationToken cancellationToken)
        {
            var answers = await _commentRepository.GetAnswersForCommentByIdAsync(request.ProfileCommentID);

            var result = answers.Select(a => new GetAnswersForCommentQueryResult
            {
                AnswerID = a.AnswerID,
                AnswerText = a.AnswerText,
                View = a.View,
                LikeCount = a.LikeCount,
                Time = a.Time,
                AppUserID = a.AppUserID,
                FirstName = a.FirstName,
                SurName = a.SurName,
                UserName = a.UserName,
                
            }).ToList();

            return new CustomResult<List<GetAnswersForCommentQueryResult>>(result, HttpStatusCode.OK);
        }
    }
}
