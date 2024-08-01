using Link.Application.Common;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Features.Mediator.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.CommentQueries
{
    public class GetCommentAndAnwerQuery : IRequest<CustomResult<List<GetCommentAndAnwerQueryResult>>>
    {
        public GetCommentAndAnwerQuery(int page, int pageSize)
        {
            this.page = page;
            this.pageSize = pageSize;
        }

        public int page { get; set; }
        public int pageSize { get; set; }
    }
}