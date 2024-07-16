using FluentValidation;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.FluentValidations
{
    public class FollowValidator : AbstractValidator<Following>
    {
        public FollowValidator()
        {
            RuleFor(following => following.AppUserID)
                            .NotEmpty().WithMessage("FollowingUserId boş olamaz.")
                            .GreaterThan(0).WithMessage("FollowingUserId pozitif bir değer olmalıdır.");
        }
    }
}
