﻿using Link.Application.Common;
using Link.Application.Features.Mediator.Results.AppUserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetAppUserQuery : IRequest<CustomResult<List<GetAppUserQueryResult>>>
    {
    }
}
