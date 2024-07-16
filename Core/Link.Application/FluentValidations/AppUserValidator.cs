using FluentValidation;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.FluentValidations
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
        }
    }
}
