namespace ShortLink.Application;

internal static class Utility
{
    static Utility()
    {
    }

    public static async Task<FluentResults.Result<TValue>> Validate<TCommand, TValue>(FluentValidation.AbstractValidator<TCommand> validator, TCommand command)
    {
        FluentResults.Result<TValue> result = new FluentResults.Result<TValue>();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(instance: command);

        if (validationResult.IsValid == false)
            foreach (FluentValidation.Results.ValidationFailure? error in validationResult.Errors)
                result.WithError(errorMessage: error.ErrorMessage);
        return result;
    }
}
