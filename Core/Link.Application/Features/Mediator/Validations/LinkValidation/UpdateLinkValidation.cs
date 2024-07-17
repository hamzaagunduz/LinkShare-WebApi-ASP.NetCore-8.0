using FluentValidation;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.LinkValidation
{
    public class UpdateLinkValidation : AbstractValidator<UpdateLinkCommand>
    {
        public UpdateLinkValidation()
        {
            RuleFor(x => x.LinkeID).NotEmpty().WithMessage("ID bos");
            RuleFor(x => x.LinkName).NotEmpty().WithMessage("name bos");
            RuleFor(x => x.LinkUrl).NotEmpty().WithMessage("url bos");

        }
    }
}
