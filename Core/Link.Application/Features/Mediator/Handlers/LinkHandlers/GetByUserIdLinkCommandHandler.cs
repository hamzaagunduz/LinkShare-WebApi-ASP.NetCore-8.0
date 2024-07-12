using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Link.Application.Common;
using Link.Application.Features.Mediator.Queries.LinkQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
using Link.Application.Features.Mediator.Results.LinkResults;
using Link.Application.Interfaces;
using Link.Application.Interfaces.LinkInterfaces;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LinkHandlers
{
    public class GetByUserIdLinkCommandHandler : IRequestHandler<GetByUserIdLinkQuery, CustomResult <List<GetByUserIdLinkQueryResult>>>
    {
        private readonly ILinkRepository _repository;
        private readonly IValidator<Linke> _validator;
        private readonly IMapper _mapper;

        public GetByUserIdLinkCommandHandler(ILinkRepository repository, IValidator<Linke> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CustomResult<List<GetByUserIdLinkQueryResult>>> Handle(GetByUserIdLinkQuery request, CancellationToken cancellationToken)
        {
            var links = _repository.GetByIdUserLink(request.Id);
            var validationErrors = new List<string>();

            // Validation logic for each Linke instance in the links collection

            var result = links.Select(link => _mapper.Map<GetByUserIdLinkQueryResult>(link)).ToList();

            //var result = links.Select(link => new GetByUserIdLinkQueryResult
            //{
            //    LinkeID = link.LinkeID,
            //    LinkName = link.LinkName,
            //    LinkUrl = link.LinkUrl,
            //}).ToList();

            return new CustomResult<List<GetByUserIdLinkQueryResult>>(result, HttpStatusCode.OK);

        }

    }
}
