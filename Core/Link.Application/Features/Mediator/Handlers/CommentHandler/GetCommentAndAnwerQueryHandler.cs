using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
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
    public class GetCommentAndAnwerQueryHandler : IRequestHandler<GetCommentAndAnwerQuery, CustomResult<List<GetCommentAndAnwerQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentAndAnwerQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CustomResult<List<GetCommentAndAnwerQueryResult>>> Handle(GetCommentAndAnwerQuery request, CancellationToken cancellationToken)
        {
            var answers = await _commentRepository.GetProfileCommentsWithAnswersAsync(request.page,request.pageSize);
            return new CustomResult<List<GetCommentAndAnwerQueryResult>>(answers, HttpStatusCode.OK);

        }
    }
}
