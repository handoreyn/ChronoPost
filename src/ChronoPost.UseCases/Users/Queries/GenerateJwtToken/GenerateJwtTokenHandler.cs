using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Core.Aggregates.Events;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Extensions.CacheExtensions;
using ChronoPost.Core.Services.Jwt;
using ChronoPost.Core.Specifications.User;
using ChronoPost.Core.ValueObjects;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ChronoPost.UseCases.Users.Queries.GenerateJwtToken;

public sealed class GenerateJwtTokenHandler : IQueryHandler<GenerateJwtTokenQuery, GenerateJwtTokenQueryResponse>
{
    private readonly IReadRepository<User> _repository;
    private readonly IJwtService _jwtService;
    private readonly IDistributedCache _distributedCache;
    private readonly IOptions<JwtOptions> _options;
    private readonly IPublisher _publisher;

    public GenerateJwtTokenHandler(IReadRepository<User> repository, IJwtService jwtService, IDistributedCache distributedCache, IOptions<JwtOptions> options, IPublisher publisher)
    {
        _repository = repository;
        _jwtService = jwtService;
        _distributedCache = distributedCache;
        _options = options;
        _publisher = publisher;
    }

    public Task<GenerateJwtTokenQueryResponse> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}