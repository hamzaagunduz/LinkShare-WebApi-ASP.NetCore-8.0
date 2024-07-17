using FluentValidation;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.LinkValidation
{
    public class CreateLinkValidation : AbstractValidator<CreateLinkCommand>
    {
        public CreateLinkValidation()
        {
            RuleFor(x => x.LinkName).NotEmpty().WithMessage("bos bırakma");
            RuleFor(x => x.LinkUrl).NotEmpty().WithMessage("bos bırakma");



        }
    }
}
