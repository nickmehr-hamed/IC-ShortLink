using System.Collections;

namespace IcFramework.Logging;

public interface ILogger<T> where T : class
{
    void LogTrace(in string message,in Hashtable? parameters = null);
    void LogDebug(in string message, in Hashtable? parameters = null);
    void LogInformation(in string message, in Hashtable? parameters = null);
    void LogWarning(in string message, in Hashtable? parameters = null);
    void LogError(in Exception? exception = null, in string? message = null, in Hashtable? parameters = null);
    void LogCritical(in Exception? exception = null, in string? message = null, in Hashtable? parameters = null);
}
