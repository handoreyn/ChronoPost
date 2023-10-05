using Ardalis.SharedKernel;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Extensions.CacheExtensions;
using ChronoPost.Core.Services.Jwt;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public class RefreshTokenQueryHandler : IQueryHandler<RefreshJwtTokenQuery, RefreshJwtTokenQueryResponse>
{
    private readonly IDistributedCache _distributedCache;
    private readonly IJwtService _jwtService;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public RefreshTokenQueryHandler(IDistributedCache distributedCache, IJwtService jwtService, IOptions<JwtOptions> jwtOptions)
    {
        _distributedCache = distributedCache;
        _jwtService = jwtService;
        _jwtOptions = jwtOptions;
    }


    public async Task<RefreshJwtTokenQueryResponse> Handle(RefreshJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var payload = await _distributedCache.GetFromCache<JwtPayload>($"refresh-token-{request.UserId}", cancellationToken);
        if (payload == null) throw new InvalidRefreshTokenException();

        var token = _jwtService.GenerateAccessToken(payload);

        return new RefreshJwtTokenQueryResponse(token.AccessToken, token.RefreshToken);
    }
}