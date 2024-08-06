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
    public class CreateAppUserCommand : IRequest<CustomResult<string>>
    {
        public string FirstName { get; set; }
        public string? Password { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
