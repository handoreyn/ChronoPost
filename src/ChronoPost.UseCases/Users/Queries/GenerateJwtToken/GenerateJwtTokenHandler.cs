using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Extensions.CacheExtensions;
using ChronoPost.Core.Services.Jwt;
using ChronoPost.Core.Specifications.User;
using ChronoPost.Core.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ChronoPost.UseCases.Users.Queries.GenerateJwtToken;

public sealed class GenerateJwtTokenHandler : IQueryHandler<GenerateJwtTokenQuery, GenerateJwtTokenQueryResponse>
{
    private readonly IReadRepository<User> _repository;
    private readonly IJwtService _jwtService;
    private readonly IDistributedCache _distributedCache;
    private readonly IOptions<JwtOptions> _options;
    public GenerateJwtTokenHandler(IReadRepository<User> repository, IJwtService jwtService, IDistributedCache distributedCache, IOptions<JwtOptions> options)
    {
        _repository = repository;
        _jwtService = jwtService;
        _distributedCache = distributedCache;
        _options = options;
    }

    public async Task<GenerateJwtTokenQueryResponse> Handle(GenerateJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserByUserCredentialSpecification(new UserCredentialValueObject(request.Username, request.Password));
        var user = await _repository.FirstOrDefaultAsync(spec, cancellationToken)
                   ?? throw new UserDoesNotExistException("Incorrect username or password! User does not exist");
        var payload = new JwtPayload(user.Id, user.UserCredentials.Username);
        var token = _jwtService.GenerateAccessToken(payload);

        await _distributedCache.SetCacheAsync($"refresh-token-{user.Id}", payload,
            TimeSpan.FromMinutes(_options.Value.RefreshTokenExpiresInMinutes),
            cancellationToken);
        
        return new GenerateJwtTokenQueryResponse(token.AccessToken, token.RefreshToken);
    }
}