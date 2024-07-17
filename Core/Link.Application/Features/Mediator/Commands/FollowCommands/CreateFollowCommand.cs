using Link.Application.Common;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.FollowCommands
{
    public class CreateFollowCommand: IRequest<CustomResult<string>>
    {
        //public int FollowerUserId { get; set; } // Takip eden kullanıcının ID'si
        public int FollowingUserId { get; set; } // Takip edilen kullanıcının ID'si

    }
}
