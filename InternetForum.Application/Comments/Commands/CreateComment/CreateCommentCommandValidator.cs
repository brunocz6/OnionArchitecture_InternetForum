using FluentValidation;

namespace InternetForum.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(v => v.Body)
                .MaximumLength(255).WithMessage("Délka komentáře musí být kratší než 255 znaků.")
                .NotEmpty().WithMessage("Zadejte text komentáře.");
        }
    }
}