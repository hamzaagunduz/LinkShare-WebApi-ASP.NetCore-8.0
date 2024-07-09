using Link.Application.Features.Mediator.Results.AppUserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetByIdAppUserQuery : IRequest<GetByIdAppUserQueryResult>
    {
        public GetByIdAppUserQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
