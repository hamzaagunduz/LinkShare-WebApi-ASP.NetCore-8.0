using Link.Application.Common;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.AppUserCommands
{
    public class RemoveAppUserCommand : IRequest<CustomResult<AppUser>>
    {
        public RemoveAppUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
