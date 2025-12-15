using FluentValidation;
using Poupe.API.Models.Transaction;
using Poupe.API.Resources;

namespace Poupe.API.Validators.Transaction;

public class UpdateTransactionModelValidator : AbstractValidator<UpdateTransactionModel>
{
    public UpdateTransactionModelValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "description"))
            .MaximumLength(250)
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "description"));

        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "value"))
            .GreaterThanOrEqualTo(0.01M)
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "value"));

        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "type"))
            .IsInEnum()
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "type"));

        RuleFor(x => x.CategoryId)
            .NotNull()
            .WithMessage(string.Format(ApiMessage.Require_Warning, "categoryId"))
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(string.Format(ApiMessage.Invalid_Warning, "categoryId"));
    }
}
