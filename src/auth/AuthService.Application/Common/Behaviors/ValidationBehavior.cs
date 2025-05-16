using FluentValidation;
using MediatR;
using ValidationException = AuthService.Application.Common.Exceptions.ValidationException;

namespace AuthService.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var errors = validationResults
                .SelectMany(result => result.Errors)
                .Select(failure => failure.ErrorMessage).ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return await next(cancellationToken);
    }
}
