using IcFramework.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace IcFramework.Persistence;

public abstract class QueryUnitOfWork<T> : IQueryUnitOfWork where T : DbContext
{
    public QueryUnitOfWork(Options options) : base()
    {
        Options = options;
        if (Options.Provider == Provider.InMemory && options.InMemoryDatabaseName == null)
            throw new ArgumentNullException(nameof(options.InMemoryDatabaseName));
        if (Options.Provider != Provider.InMemory && options.ConnectionString == null)
            throw new ArgumentNullException(nameof(options.ConnectionString));
        DbContextOptionsBuilder<T>? optionsBuilder = Options.Provider switch
        {
            Provider.SqlServer => new DbContextOptionsBuilder<T>().UseSqlServer(Options.ConnectionString ?? ""),
            //Provider.MySql => new DbContextOptionsBuilder<T>().UseMySql(Options.ConnectionString ?? ""),
            //Provider.PostgreSQL => new DbContextOptionsBuilder<T>().UseOracle(Options.ConnectionString ?? ""),
            //Provider.Oracle => new DbContextOptionsBuilder<T>().UsePostgreSQL(Options.ConnectionString ?? ""),
            Provider.InMemory => new DbContextOptionsBuilder<T>().UseInMemoryDatabase(Options.InMemoryDatabaseName ?? ""),
            _ => throw new NotImplementedException(),
        };
        _databaseContext = (T)Activator.CreateInstance(typeof(string), optionsBuilder.Options);
    }

    private T? _databaseContext;
    protected Options Options { get; private set; }
    protected T? DatabaseContext { get => _databaseContext; }
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
