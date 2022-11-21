using FluentValidation;
using MaxiCrush.Application.Common.Validation;

namespace MaxiCrush.Application.Controls.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).Password();
    }
}
