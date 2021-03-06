namespace ShortLink.Persistence;

public class QueryUnitOfWork : IcFramework.Persistence.QueryUnitOfWork<QueryDatabaseContext>, IQueryUnitOfWork
{
    public QueryUnitOfWork(IcFramework.Persistence.Options options) : base(options: options)
    {
    }

    private Links.Repositories.ILinkQueryRepository _links;

    public Links.Repositories.ILinkQueryRepository Links => _links ??= new Links.Repositories.LinkQueryRepository(DatabaseContext);
}
