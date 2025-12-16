using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Poupe.API.Models.User;
using Poupe.API.Resources;

namespace Poupe.API.Validators.User;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage(model => string.Format(ApiMessage.Require_Warning, "email"))
            .EmailAddress()
            .WithMessage(model => string.Format(ApiMessage.Invalid_Warning, "email"));

        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage(model => string.Format(ApiMessage.Require_Warning, "password"));
    }
}