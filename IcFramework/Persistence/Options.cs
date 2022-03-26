using IcFramework.Persistence.Enums;

namespace IcFramework.Persistence;

public class Options 
{
    public Options()
    {
    }

    public Provider Provider { get; init; }
    public string? ConnectionString { get; init; }
    public string? InMemoryDatabaseName { get; init; }
}
