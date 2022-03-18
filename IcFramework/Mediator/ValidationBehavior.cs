namespace IcFramework.Mediator;

public class ValidationBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    public ValidationBehavior(IEnumerable<FluentValidation.IValidator<TRequest>> validators) => Validators = validators;

    protected IEnumerable<FluentValidation.IValidator<TRequest>> Validators { get; }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MediatR.RequestHandlerDelegate<TResponse> next)
    {
        if (Validators.Any())
        {
            var context = new FluentValidation.ValidationContext<TRequest>(request);
            FluentValidation.Results.ValidationResult[]? validationResults = await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            List<FluentValidation.Results.ValidationFailure>? failures = validationResults.SelectMany(current => current.Errors).Where(current => current != null).ToList();
            if (failures.Count != 0)
                throw new FluentValidation.ValidationException(errors: failures);
        }
        return await next();
    }
}
