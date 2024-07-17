using FluentValidation;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.FollowValidation
{
    public class CreateFollowValidation : AbstractValidator<CreateFollowCommand>
    {
        public CreateFollowValidation()
        {
            RuleFor(x => x.FollowingUserId).NotEmpty();


        }
    }
}