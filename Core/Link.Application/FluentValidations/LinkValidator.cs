using FluentValidation;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.FluentValidations
{
    public class LinkValidator : AbstractValidator<Linke>
    {
        public LinkValidator()
        {
            RuleFor(x => x.LinkName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(20)
            .WithName("Başlık");

            RuleFor(x => x.LinkUrl)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("URL");
        }
    }
}
