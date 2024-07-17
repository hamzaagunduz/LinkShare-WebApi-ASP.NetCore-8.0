using Link.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.LinkCommands
{
    public class UpdateLinkCommand: IRequest<CustomResult<string>>
    {
        public int LinkeID { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
    }
}
