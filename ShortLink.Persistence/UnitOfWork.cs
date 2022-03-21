namespace ShortLink.Persistence;

public class UnitOfWork : IcFramework.Persistence.UnitOfWork<DatabaseContext>, IUnitOfWork
{
    public UnitOfWork(IcFramework.Persistence.Options options) : base(options: options)
    {
    }

    private Links.Repositories.ILinkRepository _links;

    public Links.Repositories.ILinkRepository Links => _links ??= new Links.Repositories.LinkRepository(DatabaseContext);
}
