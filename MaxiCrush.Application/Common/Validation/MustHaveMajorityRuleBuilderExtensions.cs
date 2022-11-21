using FluentValidation;

namespace MaxiCrush.Application.Common.Validation;

public static class MustHaveMajorityRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, DateTime> MustHaveMajority<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder.Must(x =>
        {
            var time = DateTime.Today - x;
            var totalDays = 365.2425;
            var age = time.TotalDays / totalDays;

            return age >= 18;
        }).WithMessage("Vous devez avoir au minimum 18 ans.");
    }
}
