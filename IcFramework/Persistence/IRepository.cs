using IcFramework.Domain;

namespace IcFramework.Persistence;

public interface IRepository<T> : IQueryRepository<T> where T : IEntity
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> DeleteByIdAsync(long id);
}
