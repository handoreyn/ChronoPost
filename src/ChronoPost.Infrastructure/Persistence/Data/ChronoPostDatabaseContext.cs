using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ChronoPost.Infrastructure.Persistence.Data;

public class ChronoPostDatabaseContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    public ChronoPostDatabaseContext(DbContextOptions options, IDomainEventDispatcher dispatcher) : base(options)
    {
        Database.EnsureCreated();
        Database.Migrate();
        _dispatcher = dispatcher;
    }

    public required DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_dispatcher == null) return result;

        var entities = ChangeTracker.Entries<EntityBase>()
            .Select(o => o.Entity)
            .Where(o => o.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entities);
        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}