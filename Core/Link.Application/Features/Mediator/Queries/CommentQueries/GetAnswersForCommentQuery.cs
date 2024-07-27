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
    public class GetAnswersForCommentQuery : IRequest<CustomResult<List<GetAnswersForCommentQueryResult>>>
    {
        public GetAnswersForCommentQuery(int profileCommentID)
        {
            ProfileCommentID = profileCommentID;
        }

        public int ProfileCommentID { get; set; }
    }
}
