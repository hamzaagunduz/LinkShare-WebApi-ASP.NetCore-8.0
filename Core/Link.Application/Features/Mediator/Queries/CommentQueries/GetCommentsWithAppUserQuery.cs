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
    public class GetCommentsWithAppUserQuery : IRequest<CustomResult<List<GetCommentsWithAppUserQueryResult>>>
    {
        public GetCommentsWithAppUserQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
