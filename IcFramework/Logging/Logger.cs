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

    protected IHttpContextAccessor? HttpContextAccessor { get; private set; }

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
        static string makeTag(DictionaryEntry item) => $"<parameter><key>{ item.Key }</key><value>{ item.Value ?? "NULL" }</value></parameter>";
        string? result = parameters.Cast<DictionaryEntry>().Where(item => item.Key is not null)
            .Aggregate(new StringBuilder(), (builder, item) => builder.Append(makeTag(item))).ToString();
        return result;
    }
    #endregion /protected virtual string GetParameters(System.Collections.Hashtable parameters)

    protected void Log(in LogLevel level, in MethodBase? methodBase, in string? message, in Exception? exception = null, in Hashtable? parameters = null)
    {
        if (exception is null && string.IsNullOrWhiteSpace(message))
            return;

        string currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
        CultureInfo newCultureInfo = new(name: "en-US");
        CultureInfo currentCultureInfo = new (currentCultureName);
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

    protected abstract void LogByFavoriteLibrary(in Log log, in Exception? exception);

    public void LogInLevel(in LogLevel level, in string? message = null, in Hashtable? parameters = null, in Exception? exception = null)
    {
        StackTrace stackTrace = new();
        MethodBase? methodBase = stackTrace?.GetFrame(1)?.GetMethod() ?? default;
        Log(level, methodBase, message, exception, parameters);
    }

    public void LogTrace(in string message, in Hashtable? parameters) => LogInLevel(LogLevel.Trace, message, parameters);
    public void LogDebug(in string message, in Hashtable? parameters) => LogInLevel(LogLevel.Debug, message, parameters);
    public void LogInformation(in string message, in Hashtable? parameters) => LogInLevel(LogLevel.Information, message, parameters);
    public void LogWarning(in string message, in Hashtable? parameters) => LogInLevel(LogLevel.Warning, message, parameters);
    public void LogError(in Exception? exception = null, in string? message = null, in Hashtable? parameters = null) => LogInLevel(LogLevel.Error, message, parameters, exception);
    public void LogCritical(in Exception? exception = null, in string? message = null, in Hashtable? parameters = null) => LogInLevel(LogLevel.Critical, message, parameters, exception);
}
