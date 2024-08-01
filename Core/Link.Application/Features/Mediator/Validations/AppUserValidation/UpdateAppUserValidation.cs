using FluentValidation;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Validations.AppUserValidation
{
    public class UpdateAppUserValidation : AbstractValidator<UpdateAppUserCommand>
    {
        public UpdateAppUserValidation()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.SurName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz e-posta adresi formatı.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş olamaz.");

        }
    }
}
