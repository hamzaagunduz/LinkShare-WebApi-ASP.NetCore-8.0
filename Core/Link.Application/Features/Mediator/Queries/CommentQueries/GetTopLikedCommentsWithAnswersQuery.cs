using Link.Application.Common;
using Link.Application.Features.Mediator.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.CommentQueries
{
    public class GetTopLikedCommentsWithAnswersQuery : IRequest<CustomResult<List<GetCommentAndAnwerQueryResult>>>
    {
        public int TopCount { get; set; }

        public GetTopLikedCommentsWithAnswersQuery(int topCount)
        {
            TopCount = topCount;
        }

        public GetTopLikedCommentsWithAnswersQuery()
        {
        }
    }
}
