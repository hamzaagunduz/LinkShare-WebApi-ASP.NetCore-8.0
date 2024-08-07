using Link.Application.Features.Mediator.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.LikeCommands
{
    public class GetLikersInfQuery : IRequest<List<GetLikersQueryResult>>
    {
        public int EntityId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EntityType EntityType { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
