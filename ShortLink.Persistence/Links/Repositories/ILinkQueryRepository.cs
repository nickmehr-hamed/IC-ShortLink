namespace ShortLink.Persistence.Links.Repositories;

public interface ILinkQueryRepository : IcFramework.Persistence.IQueryRepository<Domain.Models.Link>
{
    Task<ViewModels.GetLinksQueryResponseViewModel?> GetLinkByKeyAsync(string key);
    ViewModels.GetLinksQueryResponseViewModel? GetLinkByKey(string key);
}
