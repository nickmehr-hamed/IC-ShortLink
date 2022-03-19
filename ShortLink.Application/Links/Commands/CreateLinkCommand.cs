namespace ShortLink.Application.Links.Commands;

public class CreateLinkCommand : IcFramework.Mediator.IRequest<string>
{
    public CreateLinkCommand() : base()
    {
    }
    public string Title { get; set; }
    public string Url { get; set; }
    public long OwnerId { get; set; }
}
