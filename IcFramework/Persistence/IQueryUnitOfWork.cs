namespace IcFramework.Persistence;

public interface IQueryUnitOfWork : IDisposable
{
    bool IsDisposed { get; }
}
