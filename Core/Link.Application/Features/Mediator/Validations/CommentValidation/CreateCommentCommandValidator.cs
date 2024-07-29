using FluentValidation;
using Link.Application.Features.Mediator.Commands.Comment;


namespace Link.Application.Features.Mediator.Validations.CommentValidation
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.AppUserID).NotEmpty().WithMessage("AppUserID alanı boş olamaz.");
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment alanı boş olamaz.")
                .MinimumLength(5).WithMessage("Comment alanı en az 5 karakter uzunluğunda olmalıdır.")
                .MaximumLength(100).WithMessage("Comment alanı en fazla 100 karakter uzunluğunda olmalıdır.");

        }
    }
}
