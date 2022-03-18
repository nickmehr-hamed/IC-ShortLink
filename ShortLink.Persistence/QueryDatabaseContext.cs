using Microsoft.EntityFrameworkCore;

namespace ShortLink.Persistence;

public class QueryDatabaseContext : DbContext
{
    public QueryDatabaseContext(DbContextOptions<QueryDatabaseContext> options) : base(options: options) => Database.EnsureCreated();

    public DbSet<Domain.Models.Owner> Owners { get; set; }
    public DbSet<Domain.Models.Link> Links { get; set; }
    public DbSet<Domain.Models.UsageLog> UsageLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}
