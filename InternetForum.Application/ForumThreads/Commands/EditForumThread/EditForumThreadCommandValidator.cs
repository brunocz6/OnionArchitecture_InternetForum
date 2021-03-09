using FluentValidation;

namespace InternetForum.Application.ForumThreads.Commands.EditForumThread
{
    public class EditForumThreadCommandValidator : AbstractValidator<EditForumThreadCommand>
    {
        public EditForumThreadCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Nebylo specifikováno Id vlákna příspěvků.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název vlákna musí být kratší než 100 znaků.")
                .NotEmpty().WithMessage("Zadejte název vlákna.");

            RuleFor(v => v.Description)
                .MaximumLength(1000).WithMessage("Popis vlákna musí být kratší než 1000 znaků.")
                .NotEmpty().WithMessage("Zadejte popis vlákna.");
        }
    }
}