using System.Net;
using System.Text.Json;

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
        FluentValidation.ValidationException? validationException = exception as FluentValidation.ValidationException;

        if (validationException != null)
        {
            HttpStatusCode code = HttpStatusCode.BadRequest;
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            foreach (FluentValidation.Results.ValidationFailure? error in validationException.Errors)
                result.WithError(error.ErrorMessage);
        }
        else
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            result.WithError("Internal Server Error!");
        }

        JsonSerializerOptions? options = new ()
        {
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        string resultString = JsonSerializer.Serialize(value: result, options: options);
        return context.Response.WriteAsync(resultString);
    }
}
