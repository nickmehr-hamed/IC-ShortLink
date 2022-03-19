using ShortLink.Persistence.Links.ViewModels;

namespace ShortLink.Persistence.Links.Repositories;

public class LinkQueryRepository : IcFramework.Persistence.QueryRepository<Domain.Models.Link>, ILinkQueryRepository
{
    public LinkQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public GetLinksQueryResponseViewModel? GetLinkByKey(string key)
    {
        Domain.Models.Link? link = DbSet.FirstOrDefault(current => current.Key == key);
        GetLinksQueryResponseViewModel? result = link == null ? null : new GetLinksQueryResponseViewModel
        {
            Id = link.Id,
            Title = link.Title,
            Url = link.Url,
            Key = link.Key
        };
        return result;
    }

    public async Task<GetLinksQueryResponseViewModel?> GetLinkByKeyAsync(string key) => await Task.Run(() => GetLinkByKey(key));
}
