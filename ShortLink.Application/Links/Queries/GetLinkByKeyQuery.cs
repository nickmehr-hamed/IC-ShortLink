namespace ShortLink.Application.Links.Queries;

public class GetLinkByKeyQuery :  IcFramework.Mediator.IRequest<Persistence.Links.ViewModels.GetLinkByKeyQueryResponseViewModel>
{
    public GetLinkByKeyQuery() : base()
    {
    }

    public string Key { get; set; }
}
