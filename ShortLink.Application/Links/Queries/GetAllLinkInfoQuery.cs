namespace ShortLink.Application.Links.Queries;

public class GetAllLinkInfoQuery : IcFramework.Mediator.IRequest<IEnumerable< Persistence.Links.ViewModels.GetAllLinkInfoQueryResponseViewModel>>
{
    public GetAllLinkInfoQuery() : base()
    {
    }
}
