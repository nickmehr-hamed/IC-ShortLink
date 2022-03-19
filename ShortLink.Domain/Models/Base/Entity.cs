using IcFramework.Domain;

namespace ShortLink.Domain.Models.Base;

public abstract class Entity : IEntity
{
    public long Id { get; set; }
}
