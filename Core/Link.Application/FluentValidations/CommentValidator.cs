using FluentValidation;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.FluentValidations
{
    public class CommentValidator : AbstractValidator<ProfileComment>
    {
        //public CommentValidator()
        //{
        //    RuleFor(command => command.AppUserID).NotEmpty().WithMessage("AppUserID boş olamaz.");
        //    RuleFor(command => command.Comment).NotEmpty().WithMessage("Comment alanı boş olamaz.");
        //}
    }
}
