using System.Web;
using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates.Events;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Extensions.CacheExtensions;
using ChronoPost.Core.Services.Jwt;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

public sealed class RefreshTokenQueryHandler : IQueryHandler<RefreshJwtTokenQuery, RefreshJwtTokenQueryResponse>
{
    private readonly IDistributedCache _cache;
    private readonly IJwtService _jwtService;
    private readonly IPublisher _publisher;
    private readonly IOptions<JwtOptions> _options;

    public RefreshTokenQueryHandler(IDistributedCache cache, IJwtService jwtService, IPublisher publisher, IOptions<JwtOptions> options)
    {
        _cache = cache;
        _jwtService = jwtService;
        _publisher = publisher;
        _options = options;
    }

    public async Task<RefreshJwtTokenQueryResponse> Handle(RefreshJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var payload = await _cache.GetFromCache<JwtPayload>(request.RefreshToken, cancellationToken)
            ?? throw new InvalidRefreshTokenException();

        var token = _jwtService.GenerateAccessToken(payload);
        var notification = new UserAccessTokenRefreshedEvent(request.RefreshToken, token.RefreshToken, payload, TimeSpan.FromMinutes(_options.Value.RefreshTokenExpiresInMinutes));
        await _publisher.Publish(notification, cancellationToken).ConfigureAwait(false);

        return new RefreshJwtTokenQueryResponse(token.AccessToken, HttpUtility.UrlEncode(token.RefreshToken));
    }
}