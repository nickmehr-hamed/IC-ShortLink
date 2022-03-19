namespace ShortLink.Web.Model;

public class LinkResult
{
    public long Id { get; init; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ShortKey { get; set; }
}
