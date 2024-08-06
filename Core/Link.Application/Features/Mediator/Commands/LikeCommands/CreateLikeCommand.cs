using Link.Application.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Link.Application.Features.Mediator.Commands.LikeCommands
{
    public class CreateLikeCommand : IRequest<CustomResult<string>>
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EntityType EntityType { get; set; }
    }

    public enum EntityType
    {
        Comment,
        Answer
    }
}
