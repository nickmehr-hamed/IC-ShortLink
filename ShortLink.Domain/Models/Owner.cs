using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models;

public class Owner : Base.Entity
{
    [Required]
    [MaxLength(length: 100)]
    public string Title { get; set; }

    public ICollection<Link> Links { get; set; }

}
