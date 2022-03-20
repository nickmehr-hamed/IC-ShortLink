using IcFramework.Persistence.Enums;

namespace IcFramework.Persistence;

public class Options 
{
    public Options()
    {
    }

    public Provider Provider { get; set; }
    public string? ConnectionString { get; set; }
    public string? InMemoryDatabaseName { get; set; }
}
