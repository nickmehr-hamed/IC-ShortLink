namespace ShortLink.Persistence;

public interface IQueryUnitOfWork : IcFramework.Persistence.IQueryUnitOfWork
{
    public Logs.Repositories.ILogQueryRepository Logs { get; }
}
