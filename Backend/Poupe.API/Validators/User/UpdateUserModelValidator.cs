using FluentValidation;
using Poupe.API.Models.User;
using Poupe.API.Resources;

namespace Poupe.API.Validators.User;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "name"))
            .MaximumLength(100)
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "name"));

        RuleFor(x => x.Age)
            .NotNull()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "age"))
            .GreaterThanOrEqualTo(0)
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "age"));
    }
}
