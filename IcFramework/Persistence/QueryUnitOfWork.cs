using IcFramework.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace IcFramework.Persistence;

public abstract class QueryUnitOfWork<T> : IQueryUnitOfWork where T : DbContext
{
    public QueryUnitOfWork(Options options) : base() => Options = options;

    protected Options Options { get; set; }
    private T _databaseContext;
    protected T DatabaseContext
    {
        get
        {
            if (_databaseContext == null)
            {
                DbContextOptionsBuilder<T>? optionsBuilder = new DbContextOptionsBuilder<T>();

                switch (Options.Provider)
                {
                    case Provider.SqlServer:
                        {
                            optionsBuilder.UseSqlServer(connectionString: Options.ConnectionString);
                            break;
                        }
                    case Provider.MySql:
                        {
                            //optionsBuilder.UseMySql(connectionString: Options.ConnectionString);
                            break;
                        }
                    case Provider.Oracle:
                        {
                            //optionsBuilder.UseOracle(connectionString: Options.ConnectionString);
                            break;
                        }
                    case Provider.PostgreSQL:
                        {
                            //optionsBuilder.UsePostgreSQL(connectionString: Options.ConnectionString);
                            break;
                        }
                    case Provider.InMemory:
                        {
                            optionsBuilder.UseInMemoryDatabase(databaseName: Options.InMemoryDatabaseName);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                _databaseContext = (T)Activator.CreateInstance(type: typeof(T), args: optionsBuilder.Options);
            }
            return _databaseContext;
        }
    }
    public bool IsDisposed { get; protected set; }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
            return;
        if (disposing)
            if (_databaseContext != null)
            {
                _databaseContext.Dispose();
                _databaseContext = null;
            }
        IsDisposed = true;
    }

    ~QueryUnitOfWork()
    {
        Dispose(false);
    }
}
