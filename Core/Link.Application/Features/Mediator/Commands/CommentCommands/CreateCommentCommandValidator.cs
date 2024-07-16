using FluentValidation;
using Link.Application.Features.Mediator.Commands.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Commands.CommentCommands
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.AppUserID).NotEmpty();
            RuleFor(x => x.View).NotEmpty();


        }
    }
}
