using Link.Application.Features.Mediator.Queries.LinkQueries;
using Link.Application.Features.Mediator.Results.LinkResults;
using Link.Application.Interfaces;
using Link.Application.Interfaces.LinkInterfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class GetByUserIdLinkCommandHandler : IRequestHandler<GetByUserIdLinkQuery, List<GetByUserIdLinkQueryResult>>
    {
        private readonly ILinkRepository _repository;

        public GetByUserIdLinkCommandHandler(ILinkRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetByUserIdLinkQueryResult>> Handle(GetByUserIdLinkQuery request, CancellationToken cancellationToken)
        {
            var links = _repository.GetByIdUserLink(request.Id);

            var result = links.Select(link => new GetByUserIdLinkQueryResult
            {
                LinkeID = link.LinkeID,
                LinkName = link.LinkName,
                LinkUrl = link.LinkUrl,
            }).ToList();

            return await Task.FromResult(result);
        }
    }
}
