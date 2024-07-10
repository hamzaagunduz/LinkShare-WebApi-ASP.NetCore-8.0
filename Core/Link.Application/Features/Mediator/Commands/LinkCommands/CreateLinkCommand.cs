using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.LinkCommands
{
    public class CreateLinkCommand:IRequest
    {
        public int AppUserID { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
    }
}
