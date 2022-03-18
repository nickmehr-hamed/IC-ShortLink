namespace ShortLink.Persistence;

public interface IUnitOfWork : IcFramework.Persistence.IUnitOfWork
{
    public Logs.Repositories.ILogRepository Logs { get; }
}
