using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Persistence.Links.ViewModels;
public class GetAllLinkInfoQueryResponseViewModel
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string Url { get; init; }
    public string ShortKey { get; init; }
    public string OwnerTitle { get; set; }
    public int UsageCount { get; set; }
}
