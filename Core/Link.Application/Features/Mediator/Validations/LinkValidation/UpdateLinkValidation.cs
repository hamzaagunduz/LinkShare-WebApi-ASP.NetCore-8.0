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
            RuleFor(x => x.LinkeID).NotEmpty().WithMessage("ID boş olamaz");


            RuleFor(x => x.LinkName)
                .NotEmpty().WithMessage("Link Adı Boş Olamaz")
                .MaximumLength(10).WithMessage("Link Adı en fazla 10 karakter olmalıdır");

            RuleFor(x => x.LinkUrl)
                .NotEmpty().WithMessage("Link URL bırakılamaz")
                .MaximumLength(2000).WithMessage("Link URL en fazla 2000 karakter olabilir");

        }


        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
