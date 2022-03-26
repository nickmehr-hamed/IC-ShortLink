using Microsoft.AspNetCore.Http;

namespace IcFramework.Logging;

public class Log4NetAdapter<T> : Logger<T> where T : class
{
    public Log4NetAdapter(in IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    protected override void LogByFavoriteLibrary(in Log log, in Exception? exception) => throw new NotImplementedException();
}
