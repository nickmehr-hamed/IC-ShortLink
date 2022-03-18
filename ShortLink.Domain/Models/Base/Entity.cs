using IcFramework.Domain;

namespace ShortLink.Domain.Models.Base
{
    public abstract class Entity : IEntity
    {
        long IEntity.Id { get; set; }
    }
}
