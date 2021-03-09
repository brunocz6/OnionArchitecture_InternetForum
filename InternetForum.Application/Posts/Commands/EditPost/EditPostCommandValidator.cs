using FluentValidation;
using InternetForum.Application.Posts.Commands.EditPost;

namespace InternetForum.Application.Posts.Commands.EditPost
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Nebylo určeno Id příspěvku.");
            
            RuleFor(v => v.ForumThreadId)
                .NotEmpty().WithMessage("Nebylo určeno vlákno příspěvku.");
            
            RuleFor(v => v.Title)
                .MaximumLength(255).WithMessage("Nadpis příspěvku musí být kratší než 255 znaků.")
                .NotEmpty().WithMessage("Zadejte nadpis příspěvku.");

            RuleFor(v => v.Body)
                .MaximumLength(5000).WithMessage("Text příspěvku musí být kratší než 5000 znaků.")
                .NotEmpty().WithMessage("Zadejte text příspěvku.");
        }
    }
}