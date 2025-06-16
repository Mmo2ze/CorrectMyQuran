using CorectMyQuran.Application.ValidationErrors;
using CorectMyQuran.DateBase.Common.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace CorectMyQuran.Application.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder, ValidationError error)
    {
        ruleBuilder.WithMessage(error.Description);
        ruleBuilder.OverridePropertyName(error.Code);
        ruleBuilder.WithErrorCode(ErrorType.Validation.ToString());

        ruleBuilder.WithSeverity(Severity.Info);
        return ruleBuilder;
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder, Error error)
    {
        ruleBuilder.WithMessage(error.Description);
        ruleBuilder.OverridePropertyName(error.Code);
        ruleBuilder.WithErrorCode(error.Type.ToString());
        return ruleBuilder;
    }


    public static IRuleBuilderOptions<T, string> Length<T>(this IRuleBuilder<T, string> ruleBuilder, Length length)
        => ruleBuilder.SetValidator(new LengthValidator<T>(length.Min, length.Max));
}