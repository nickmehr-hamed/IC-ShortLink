namespace ShortLink.Persistence;

public interface IUnitOfWork : IcFramework.Persistence.IUnitOfWork
{
    public Links.Repositories.ILinkRepository Links { get; }
}
