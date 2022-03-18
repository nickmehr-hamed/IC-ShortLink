using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models;

public class UsageLog : Base.Entity
{
    [Required]
    public DateTime TimeStamp { get; set; }

    public long LinkId { get; set; }

    [Required]
    [MaxLength(length: 15)]
    public string RemoteIp { get; set; }

    [MaxLength(length: 1000)]
    public string HttpReferrer { get; set; }
}
