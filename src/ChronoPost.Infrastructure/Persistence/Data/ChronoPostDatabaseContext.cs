using ChronoPost.Core.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ChronoPost.Infrastructure.Persistence.Data;

public class ChronoPostDatabaseContext : DbContext
{
    public ChronoPostDatabaseContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
        Database.Migrate();
    }

    public required DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}