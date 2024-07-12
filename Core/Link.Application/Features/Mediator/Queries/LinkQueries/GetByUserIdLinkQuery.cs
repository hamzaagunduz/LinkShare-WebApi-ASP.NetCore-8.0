using Link.Application.Common;
using Link.Application.Features.Mediator.Results.LinkResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.LinkQueries
{
    public class GetByUserIdLinkQuery:IRequest<CustomResult<List<GetByUserIdLinkQueryResult>>>
    {
        public GetByUserIdLinkQuery(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }
    }
}
