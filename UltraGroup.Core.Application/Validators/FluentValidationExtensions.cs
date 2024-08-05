using FluentValidation;
using System.Globalization;

namespace UltraGroup.Core.Application.Validators
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptionsConditions<T, TElement> DateStringFormat<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder, string format)
        {
            return ruleBuilder.Custom((dateString, context) =>
            {
                DateTime date = DateTime.Now;
                var res = DateTime.TryParseExact(dateString.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!res)
                    context.AddFailure($"The date has not the correct format: {format}");
            });
        }
    }
}
