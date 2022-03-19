namespace ShortLink.Api.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware : object
{
    public ExceptionHandlingMiddleware(RequestDelegate next) : base() => Next = next;

    protected RequestDelegate Next { get; }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        FluentResults.Result result = new FluentResults.Result();
        FluentValidation.ValidationException validationException = exception as FluentValidation.ValidationException;

        if (validationException != null)
        {
            System.Net.HttpStatusCode code = System.Net.HttpStatusCode.BadRequest;
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            foreach (FluentValidation.Results.ValidationFailure? error in validationException.Errors)
                result.WithError(error.ErrorMessage);
        }
        else
        {
            System.Net.HttpStatusCode code = System.Net.HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            result.WithError("Internal Server Error!");
        }

        System.Text.Json.JsonSerializerOptions? options = new System.Text.Json.JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
        };

        string resultString = System.Text.Json.JsonSerializer.Serialize(value: result, options: options);
        return context.Response.WriteAsync(resultString);
    }
}
