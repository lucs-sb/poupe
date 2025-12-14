using FluentValidation;
using Poupe.API.Models.User;

namespace Poupe.API.Validators.User;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Age)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}
