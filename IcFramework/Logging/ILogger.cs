using System.Collections;

namespace IcFramework.Logging;

public interface ILogger<T> where T : class
{
    void LogTrace(string message, Hashtable? parameters = null);
    void LogDebug(string message, Hashtable? parameters = null);
    void LogInformation(string message, Hashtable? parameters = null);
    void LogWarning(string message, Hashtable? parameters = null);
    void LogError(Exception? exception = null, string? message = null, Hashtable? parameters = null);
    void LogCritical(Exception? exception = null, string? message = null, Hashtable? parameters = null);
}
