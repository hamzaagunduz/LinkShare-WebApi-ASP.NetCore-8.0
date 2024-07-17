using FluentValidation;
using Link.Application.Features.Mediator.Commands.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.CommentValidation
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.AppUserID).NotEmpty();
            RuleFor(x => x.Comment)
                        .MinimumLength(5).WithMessage("5kara.");

        }
    }
}
