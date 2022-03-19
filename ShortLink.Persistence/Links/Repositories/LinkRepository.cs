using Microsoft.EntityFrameworkCore;

namespace ShortLink.Persistence.Links.Repositories;

public class LinkRepository : IcFramework.Persistence.Repository<Domain.Models.Link>, ILinkRepository
{
    protected internal LinkRepository(DbContext databaseContext) : base(databaseContext)
    {
    }
}
