namespace ShortLink.Persistence;

public class UnitOfWork : IcFramework.Persistence.UnitOfWork<DatabaseContext>, IUnitOfWork
{
    public UnitOfWork(IcFramework.Persistence.Options options) : base(options: options)
    {
    }

    private Logs.Repositories.ILogRepository _logs;

    public Logs.Repositories.ILogRepository Logs
    {
        get
        {
            _logs = _logs ?? new Logs.Repositories.ILogQueryRepository(DatabaseContext);
            return _logs;
        }
    }
}
