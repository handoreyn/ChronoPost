using Ardalis.SharedKernel;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ChronoPost.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ChronoPost.Infrastructure.Persistence.Repositories;

public sealed class ReadRepository<TAggregateRoot> : RepositoryBase<TAggregateRoot>, IReadRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
{
    public ReadRepository(ChronoPostDatabaseContext dbContext) : base(dbContext)
    {
    }
}