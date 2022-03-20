using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace IcFramework.Logging;

public abstract class Logger<T> : object, ILogger<T> where T : class
{
    protected Logger(IHttpContextAccessor? httpContextAccessor = null) : base() => HttpContextAccessor = httpContextAccessor;

    protected IHttpContextAccessor? HttpContextAccessor { get; set; }

    #region GetExceptions(System.Exception exception)
    protected virtual string GetExceptions(in Exception? exception)
    {
        StringBuilder result = new();
        Exception? currentException = exception;
        string tag = nameof(Exception);
        while (currentException != null)
        {
            result.Append($"<{ tag }>{currentException.Message}</{ tag }>");
            tag = nameof(Exception.InnerException);
            currentException = currentException.InnerException;
        }
        return result.ToString();
    }
    #endregion /GetExceptions(System.Exception exception)

    #region protected virtual string GetParameters(System.Collections.Hashtable parameters)
    protected virtual string? GetParameters(in Hashtable? parameters)
    {
        if (parameters?.Count is null or 0)
            return null;

        string? result = parameters.Cast<DictionaryEntry>().Where(item => item.Key is not null)
            .Aggregate(new StringBuilder(),
            (builder, item) => builder.Append($"<parameter><key>{ item.Key }</key><value>{ item.Value ?? "NULL" }</value></parameter>"))
            .ToString();
        return result;
    }
    #endregion /protected virtual string GetParameters(System.Collections.Hashtable parameters)

    protected void Log(LogLevel level, MethodBase? methodBase, string? message, Exception? exception = null, Hashtable? parameters = null)
    {
        if (exception is null && string.IsNullOrWhiteSpace(message))
            return;

        string currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
        CultureInfo newCultureInfo = new CultureInfo(name: "en-US");
        CultureInfo currentCultureInfo = new CultureInfo(currentCultureName);
        Thread.CurrentThread.CurrentCulture = newCultureInfo;

        Log log = new()
        {
            Level = level,
            ClassName = typeof(T).Name,
            MethodName = methodBase?.Name,
            Namespace = typeof(T).Namespace,
            Message = message,
            Exceptions = GetExceptions(exception),
            Parameters = GetParameters(parameters),
            Username = HttpContextAccessor?.HttpContext?.User?.Identity?.Name,
            RemoteIP = HttpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
            RequestPath = HttpContextAccessor?.HttpContext?.Request?.Path,
            HttpReferrer = HttpContextAccessor?.HttpContext?.Request?.Headers["Referer"]
        };

        LogByFavoriteLibrary(log, exception);
        Thread.CurrentThread.CurrentCulture = currentCultureInfo;
    }

    protected abstract void LogByFavoriteLibrary(Log log, Exception? exception);

    public void LogInLevel(LogLevel level, string? message = null, Hashtable? parameters = null, Exception? exception = null)
    {
        StackTrace stackTrace = new();
        MethodBase? methodBase = stackTrace?.GetFrame(1)?.GetMethod() ?? default(MethodBase);
        Log(level, methodBase, message, exception, parameters);
    }

    public void LogTrace(string message, Hashtable? parameters) => LogInLevel(LogLevel.Trace, message, parameters);
    public void LogDebug(string message, Hashtable? parameters) => LogInLevel(LogLevel.Debug, message, parameters);
    public void LogInformation(string message, Hashtable? parameters) => LogInLevel(LogLevel.Information, message, parameters);
    public void LogWarning(string message, Hashtable? parameters) => LogInLevel(LogLevel.Warning, message, parameters);
    public void LogError(Exception? exception = null, string? message = null, Hashtable? parameters = null) => LogInLevel(LogLevel.Error, message, parameters, exception);
    public void LogCritical(Exception? exception = null, string? message = null, Hashtable? parameters = null) => LogInLevel(LogLevel.Critical, message, parameters, exception);
}
