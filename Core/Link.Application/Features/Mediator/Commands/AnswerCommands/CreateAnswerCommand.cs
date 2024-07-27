using Link.Application.Common;
using Link.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.AnswerCommands
{
    public class CreateAnswerCommand : IRequest<CustomResult<string>>
    {
        public CreateAnswerCommand(int profileCommentID, string answerText)
        {
            ProfileCommentID = profileCommentID;
            AnswerText = answerText;
        }

        public int ProfileCommentID { get; set; }

        public string AnswerText { get; set; }
    }
}
