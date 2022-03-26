using IcFramework.Domain;
using Microsoft.EntityFrameworkCore;

namespace IcFramework.Persistence;

public abstract class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected internal Repository(in DbContext databaseContext) : base()
    {
        DatabaseContext = databaseContext ?? throw new ArgumentNullException(paramName: nameof(databaseContext));
        DbSet = DatabaseContext.Set<T>();
    }

    protected DbSet<T> DbSet { get; }
    protected DbContext DatabaseContext { get; }

    public virtual async Task InsertAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException( nameof(entity));
        await DbSet.AddAsync(entity);
    }
    protected virtual void Update(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        DbSet.Update(entity);
    }
    public virtual async Task UpdateAsync(T entity) => await Task.Run(() => { DbSet.Update(entity); });
        public virtual async Task DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(paramName: nameof(entity));
        await Task.Run(() => { DbSet.Remove(entity); });
    }

    public virtual async Task<T?> GetByIdAsync(long id) => await DbSet.FindAsync(keyValues: id);

    public virtual async Task<bool> DeleteByIdAsync(long id)
    {
        T? entity = await GetByIdAsync(id);
        if (entity == null)
            return false;
        await DeleteAsync(entity);
        return true;
    }

    public virtual async Task<IList<T>> GetAllAsync()
    {
        List<T>? result = await DbSet.ToListAsync();
        return result;
    }
}
