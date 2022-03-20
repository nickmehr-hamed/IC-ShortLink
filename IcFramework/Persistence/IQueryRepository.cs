using IcFramework.Domain;

namespace IcFramework.Persistence;

public interface IQueryRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(long id);
    Task<IList<T>> GetAllAsync();
}
