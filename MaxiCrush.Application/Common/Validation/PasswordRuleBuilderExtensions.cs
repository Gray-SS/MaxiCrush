using FluentValidation;

namespace MaxiCrush.Application.Common.Validation;

public static class PasswordValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Length(10, 24)
                          .Must(x => x.Count(char.IsDigit) > 3)
                          .WithMessage("Le mot de passe doit contenir au minimum 3 chiffres")
                          .Must(x => x.Any(char.IsPunctuation))
                          .WithMessage("Le mot de passe doit contenir une ponctuation au minimum");
    }
}
