using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.LinkCommands
{
    public class RemoveLinkCommand:IRequest
    {
        public RemoveLinkCommand(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
