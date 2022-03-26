using Microsoft.AspNetCore.Http;

namespace IcFramework.Logging;

public class NLogAdapter<T> : Logger<T> where T : class
{
    public NLogAdapter(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override void LogByFavoriteLibrary(in Log log, in Exception? exception)
    {
        string loggerMessage = log.ToString();
        NLog.Logger logger = NLog.LogManager.GetLogger(name: typeof(T).ToString());
        Action<Exception?, string> logAction = log.Level switch
        {
            LogLevel.Trace => logger.Trace,
            LogLevel.Debug => logger.Debug,
            LogLevel.Information => logger.Info,
            LogLevel.Warning => logger.Warn,
            LogLevel.Error => logger.Error,
            LogLevel.Critical => logger.Fatal,
            _ => throw new NotImplementedException()
        };
        logAction(exception, loggerMessage);
    }
}
