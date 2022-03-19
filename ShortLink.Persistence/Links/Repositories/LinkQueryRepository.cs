using ShortLink.Persistence.Links.ViewModels;

namespace ShortLink.Persistence.Links.Repositories;

public class LinkQueryRepository : IcFramework.Persistence.QueryRepository<Domain.Models.Link>, ILinkQueryRepository
{
    public LinkQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public GetLinkByKeyQueryResponseViewModel? GetLinkByKey(string key)
    {
        Domain.Models.Link? link = DbSet.FirstOrDefault(current => current.ShortKey == key);
        GetLinkByKeyQueryResponseViewModel? result = link == null ? null : new ()
        {
            Id = link.Id,
            Title = link.Title,
            Url = link.Url,
            ShortKey = link.ShortKey
        };
        return result;
    }

    public async Task<GetLinkByKeyQueryResponseViewModel?> GetLinkByKeyAsync(string key) => await Task.Run(() => GetLinkByKey(key));
}
