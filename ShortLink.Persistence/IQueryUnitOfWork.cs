namespace ShortLink.Persistence;

public interface IQueryUnitOfWork : IcFramework.Persistence.IQueryUnitOfWork
{
    public Links.Repositories.ILinkQueryRepository Links { get; }
}
