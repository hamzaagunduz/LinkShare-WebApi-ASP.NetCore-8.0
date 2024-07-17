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
        public CreateCommentCommand(int appUserID, string comment, int view, int like, bool hidden, DateTime time)
        {
            AppUserID = appUserID;
            Comment = comment;
            View = view;
            Like = like;
            Hidden = hidden;
            Time = time;
        }

        public int AppUserID { get; set; }

        public string Comment { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }
    }
}