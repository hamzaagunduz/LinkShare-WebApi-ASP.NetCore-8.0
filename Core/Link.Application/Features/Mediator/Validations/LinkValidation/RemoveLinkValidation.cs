using FluentValidation;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.LinkValidation
{
    public class RemoveLinkValidation : AbstractValidator<RemoveLinkCommand>
    {
        public RemoveLinkValidation()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
