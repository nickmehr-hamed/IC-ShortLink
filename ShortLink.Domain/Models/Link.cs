using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models;

public class Link : Base.Entity
{
    [Required]
    [MaxLength(length: 100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(length: 1000)]
    public string Url { get; set; }

    [Required]
    public long OwnerId { get; set; }

    [Required]
    [MaxLength(length: 10)]
    public string  Tocken { get; set; }
}
