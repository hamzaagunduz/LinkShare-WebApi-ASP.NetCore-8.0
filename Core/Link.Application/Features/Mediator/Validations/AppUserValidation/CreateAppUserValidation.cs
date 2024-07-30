using FluentValidation;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Dto.AppUserDtos;

public class CreateAppUserValidation : AbstractValidator<CreateAppUserCommand>
{
    public CreateAppUserValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı boş olamaz.")
            .MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakter olabilir.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
            .MinimumLength(60).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.");

        RuleFor(x => x.SurName)
            .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
            .MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakter olabilir.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
            .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.");
    }
}
