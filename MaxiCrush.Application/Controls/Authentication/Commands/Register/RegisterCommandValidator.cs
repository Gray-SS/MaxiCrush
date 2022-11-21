using FluentValidation;
using MaxiCrush.Application.Common.Validation;

namespace MaxiCrush.Application.Controls.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Firstname).MinimumLength(2);
        RuleFor(x => x.Lastname).MinimumLength(2);
        RuleFor(x => x.Password).Password();
        RuleFor(x => x.Birthday).MustHaveMajority();
    }
}
