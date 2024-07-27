using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.CommentCommands
{
    public class RemoveCommentCommand:IRequest
    {
        public RemoveCommentCommand(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
