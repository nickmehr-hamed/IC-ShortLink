using IcFramework.Domain;
using Microsoft.EntityFrameworkCore;

namespace IcFramework.Persistence;

public abstract class QueryRepository<TEntity> :
    object, IQueryRepository<TEntity> where TEntity : class, IEntity
{
    protected QueryRepository(DbContext databaseContext)
    {
        DatabaseContext = databaseContext ?? throw new ArgumentNullException(paramName: nameof(databaseContext));
        DbSet = DatabaseContext.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet { get; }

    protected DbContext DatabaseContext { get; }

    public virtual async Task<TEntity> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
        List<TEntity>? result = await DbSet.ToListAsync();
        return result;
    }
}
