namespace ShortLink.Persistence;

public class QueryUnitOfWork : IcFramework.Persistence.QueryUnitOfWork<QueryDatabaseContext>, IQueryUnitOfWork
{
    public QueryUnitOfWork(IcFramework.Persistence.Options options) : base(options: options)
    {
    }

    private Logs.Repositories.ILogQueryRepository _logs;

    public Logs.Repositories.ILogQueryRepository Logs
    {
        get
        {
            _logs = _logs ?? new Logs.Repositories.ILogQueryRepository(DatabaseContext);
            return _logs;
        }
    }
}
