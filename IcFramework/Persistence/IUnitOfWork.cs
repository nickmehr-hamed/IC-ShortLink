namespace IcFramework.Persistence;

public interface IUnitOfWork : IQueryUnitOfWork
{
    Task SaveAsync();
}
