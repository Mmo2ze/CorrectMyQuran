using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CorectMyQuran.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.IsValid)
        {
            return await next();
        }

        var errors = MapValidatorErrors(validatorResult.Errors);
        return (dynamic)errors;
    }

    private List<Error> MapValidatorErrors(IList<ValidationFailure> validatorFailures)
    {
        var errors = new List<Error>();
        foreach (var validatorFailure in validatorFailures)
        {
            var error = MapErrorCodeToErrorType(validatorFailure);
            errors.Add(error);
        }

        return errors;
    }

    private Error MapErrorCodeToErrorType(ValidationFailure error)
    {
        switch (error.ErrorCode)
        {
            case "Failure":
                return Error.Failure(error.PropertyName, error.ErrorMessage);
            case "Unexpected":
                return Error.Unexpected(error.PropertyName, error.ErrorMessage);
            case "Validation":
                return Error.Validation(error.PropertyName, error.ErrorMessage);
            case "Conflict":
                return Error.Conflict(error.PropertyName, error.ErrorMessage);
            case "NotFound":
                return Error.NotFound(error.PropertyName, error.ErrorMessage);
            case "Unauthorized":
                return Error.Unauthorized(error.PropertyName, error.ErrorMessage);
            case "Forbidden":
                return Error.Forbidden(error.PropertyName, error.ErrorMessage);

            default:
                return Error.Validation(error.PropertyName, error.ErrorMessage);
        }
    }
}