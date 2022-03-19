namespace ShortLink.Persistence.Links.ViewModels;

public class GetLinksQueryResponseViewModel : object
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string Url { get; init; }
    public string Key { get; init; }
}
