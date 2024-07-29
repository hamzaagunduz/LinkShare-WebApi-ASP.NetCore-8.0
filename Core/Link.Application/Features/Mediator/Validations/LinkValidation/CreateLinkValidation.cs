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
                .MaximumLength(10).WithMessage("Link Adı en fazla 10 karakter olmalıdır");

            RuleFor(x => x.LinkUrl)
                .NotEmpty().WithMessage("Link URL Boş bırakılamaz")
                .MaximumLength(2000).WithMessage("Link URL en fazla 2000 karakter olabilir");
        }
    }
}