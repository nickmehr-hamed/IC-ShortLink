using FluentValidation;
using FluentValidation.Results;

namespace IcFramework.Mediator;

public class ValidationBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => Validators = validators;

    protected IEnumerable<IValidator<TRequest>> Validators { get; }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MediatR.RequestHandlerDelegate<TResponse> next)
    {
        if (Validators.Any())
        {
            ValidationContext<TRequest>? context = new (request);
            ValidationResult[]? validationResults = await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            List<ValidationFailure>? failures = validationResults.SelectMany(current => current.Errors).Where(current => current != null).ToList();
            if (failures.Any())
                throw new ValidationException(errors: failures);
        }
        return await next();
    }
}
