using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models;

public class UsageLog : Base.Entity
{
    public long LinkId { get; set; }

    [Required]
    public DateTime TimeStamp { get; set; }

    public Link Link { get; set; }
}
