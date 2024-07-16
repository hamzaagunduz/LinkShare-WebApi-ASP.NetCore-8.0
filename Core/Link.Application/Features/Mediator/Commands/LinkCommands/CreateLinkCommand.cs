using Link.Application.Common;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.LinkCommands
{
    public class CreateLinkCommand: IRequest<CustomResult<Linke>>
    {
        //public int AppUserID { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
    }
}
