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
    public class UpdateAppUserCommand: IRequest<CustomResult<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }

        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string About { get; set; }



    }
}
