namespace IcFramework.Persistence;

public abstract class UnitOfWork<T> :
    QueryUnitOfWork<T>, IUnitOfWork where T : Microsoft.EntityFrameworkCore.DbContext
{
    public UnitOfWork(Options options) : base(options: options)
    {
    }

    public async Task SaveAsync()
    {
        await DatabaseContext.SaveChangesAsync();
    }
}
