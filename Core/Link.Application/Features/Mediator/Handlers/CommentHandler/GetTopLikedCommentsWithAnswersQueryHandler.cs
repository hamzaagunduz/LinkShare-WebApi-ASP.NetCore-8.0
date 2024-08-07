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
    public class GetTopLikedCommentsWithAnswersQueryHandler : IRequestHandler<GetTopLikedCommentsWithAnswersQuery, CustomResult<List<GetCommentAndAnwerQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetTopLikedCommentsWithAnswersQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CustomResult<List<GetCommentAndAnwerQueryResult>>> Handle(GetTopLikedCommentsWithAnswersQuery request, CancellationToken cancellationToken)
        {
            var result = await _commentRepository.GetTopLikedCommentsWithAnswersAsync(request.TopCount);

            // Başarı durumunda CustomResult ile yanıt oluştur
            return new CustomResult<List<GetCommentAndAnwerQueryResult>>(
                data: result,
                statusCode: HttpStatusCode.OK
            );
        }
    }
}

