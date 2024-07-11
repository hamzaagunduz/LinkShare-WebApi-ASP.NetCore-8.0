﻿using Link.Application.Common;
using Link.Application.Features.Mediator.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.CommentQueries
{
    public class GetByAppUserIDCommentQuery:IRequest<CustomResult<List<GetByAppUserIDCommentQueryResult>>>
    {
        public GetByAppUserIDCommentQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
