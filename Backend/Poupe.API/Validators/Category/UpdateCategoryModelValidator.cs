using FluentValidation;
using Poupe.API.Models.Category;
using Poupe.API.Resources;

namespace Poupe.API.Validators.Category;

public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
{
    public UpdateCategoryModelValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "description"))
            .MaximumLength(250)
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "description"));

        RuleFor(x => x.Purpose)
            .NotNull()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "purpose"))
            .IsInEnum()
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "purpose"));
    }
}