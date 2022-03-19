namespace ShortLink.Api.Infrastructure.Middlewares;

public static class MiddlewareExtensions
{
    static MiddlewareExtensions()
    {
    }

    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionHandlingMiddleware>();
}
