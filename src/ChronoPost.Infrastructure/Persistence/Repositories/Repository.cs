using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using ChronoPost.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ChronoPost.Infrastructure.Persistence.Repositories;

public sealed class Repository<TAggregateRoot> : RepositoryBase<TAggregateRoot>, IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
{
    public Repository(ChronoPostDatabaseContext dbContext) : base(dbContext)
    {
    }
}