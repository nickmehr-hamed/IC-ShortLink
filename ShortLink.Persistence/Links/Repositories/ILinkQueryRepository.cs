namespace ShortLink.Persistence.Links.Repositories;

public interface ILinkQueryRepository : IcFramework.Persistence.IQueryRepository<Domain.Models.Link>
{
    Task<ViewModels.GetLinkByKeyQueryResponseViewModel?> GetLinkByKeyAsync(string key);
    ViewModels.GetLinkByKeyQueryResponseViewModel? GetLinkByKey(string key);
    Task<IEnumerable< ViewModels.GetAllLinkInfoQueryResponseViewModel>> GetAllLinkInfoAsync();
    IEnumerable<ViewModels.GetAllLinkInfoQueryResponseViewModel> GetAllLinkInfo();
}
