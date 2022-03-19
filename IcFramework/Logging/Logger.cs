using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace IcFramework.Logging;

public abstract class Logger<T> : object, ILogger<T> where T : class
{
    protected Logger(IHttpContextAccessor httpContextAccessor = null) : base() => HttpContextAccessor = httpContextAccessor;

    protected IHttpContextAccessor HttpContextAccessor { get; set; }

    #region GetExceptions(System.Exception exception)
    //TODO: refactor
    protected virtual string GetExceptions(Exception exception)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        Exception currentException = exception;
        int index = 0;

        while (currentException != null)
        {
            if (index == 0)
                result.Append($"<{ nameof(Exception) }>");
            else
                result.Append($"<{ nameof(Exception.InnerException) }>");

            result.Append(currentException.Message);

            if (index == 0)
                result.Append($"</{ nameof(Exception) }>");
            else
                result.Append($"</{ nameof(Exception.InnerException) }>");
            index++;

            currentException = currentException.InnerException;
        }

        return result.ToString();
    }
    #endregion /GetExceptions(System.Exception exception)

    #region protected virtual string GetParameters(System.Collections.Hashtable parameters)
    //TODO: refactor
    protected virtual string GetParameters(System.Collections.Hashtable parameters)
    {
        if (parameters == null || parameters.Count == 0)
            return null;

        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        foreach (System.Collections.DictionaryEntry item in parameters)
        {
            if (item.Key != null)
            {
                stringBuilder.Append("<parameter>");
                stringBuilder.Append($"<key>{ item.Key }</key>");
                if (item.Value == null)
                    stringBuilder.Append($"<value>NULL</value>");
                else
                    stringBuilder.Append($"<value>{ item.Value }</value>");
                stringBuilder.Append("</parameter>");
            }
        }
        string result = stringBuilder.ToString();
        return result;
    }
    #endregion /protected virtual string GetParameters(System.Collections.Hashtable parameters)

    //TODO: refactor
    protected void Log(LogLevel level, MethodBase methodBase, string message, Exception exception = null, Hashtable parameters = null)
    {
        if (exception == null && string.IsNullOrWhiteSpace(message))
            return;

        string currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
        CultureInfo newCultureInfo = new CultureInfo(name: "en-US");
        CultureInfo currentCultureInfo = new CultureInfo(currentCultureName);
        Thread.CurrentThread.CurrentCulture = newCultureInfo;

        Log log = new Log
        {
            Level = level,
            ClassName = typeof(T).Name,
            MethodName = methodBase.Name,
            Namespace = typeof(T).Namespace
        };

        if (HttpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress != null)
            log.RemoteIP = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        if (HttpContextAccessor?.HttpContext?.User?.Identity != null)
            log.Username = HttpContextAccessor.HttpContext.User.Identity.Name;

        if (HttpContextAccessor?.HttpContext?.Request != null)
        {
            log.RequestPath = HttpContextAccessor.HttpContext.Request.Path;
            log.HttpReferrer = HttpContextAccessor.HttpContext.Request.Headers["Referer"];
        }

        log.Message = message;

        log.Exceptions = GetExceptions(exception: exception);
        log.Parameters = GetParameters(parameters: parameters);
        LogByFavoriteLibrary(log: log, exception: exception);
        Thread.CurrentThread.CurrentCulture = currentCultureInfo;
    }

    protected abstract void LogByFavoriteLibrary(Log log, Exception exception);

    public void LogTrace(string message, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Trace, message: message, exception: null, parameters: parameters);
    }

    public void LogDebug(string message, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Debug, message: message, exception: null, parameters: parameters);
    }

    public void LogInformation(string message, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Information, message: message, exception: null, parameters: parameters);
    }

    public void LogWarning
        (string message, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Warning, message: message, exception: null, parameters: parameters);
    }

    public void LogError(Exception exception = null, string message = null, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Error, message: message, exception: exception, parameters: parameters);
    }

    public void LogCritical(Exception exception = null, string message = null, System.Collections.Hashtable parameters = null)
    {
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        Log(methodBase: methodBase, level: LogLevel.Critical, message: message, exception: exception, parameters: parameters);
    }
}
