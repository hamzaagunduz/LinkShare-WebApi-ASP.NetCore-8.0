using FluentValidation;
using Link.Dto.LinkDto;

namespace Link.Application.Features.Mediator.Validations.LinkValidation
{
    public class CreateLinkValidation : AbstractValidator<AddLinkDto>
    {
        public CreateLinkValidation()
        {
            RuleFor(x => x.LinkName)
                .NotEmpty().WithMessage("Link Adı Boş Olamaz")
                .MaximumLength(15).WithMessage("Link Adı en fazla 10 karakter olmalıdır");

            RuleFor(x => x.LinkUrl)
                     .NotEmpty().WithMessage("Link URL Boş bırakılamaz")
                     .MaximumLength(200).WithMessage("Link URL en fazla 2000 karakter olabilir")
                     .Matches(@"^https?:\/\/[^\s$.?#].[^\s]*$").WithMessage("Geçerli bir URL girin.");

        }
    }
}