using Link.Application.Common;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.Comment
{
    public class CreateCommentCommand : IRequest<CustomResult<string>>
    {
        public CreateCommentCommand(int appUserID, string comment)
        {
            AppUserID = appUserID;
            Comment = comment;

        }

        public int AppUserID { get; set; }

        public string Comment { get; set; }

    }
}