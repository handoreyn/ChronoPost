using System.Text;
using System.Text.Json;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Extensions.CacheExtensions;
using ChronoPost.Core.Services.Jwt;
using ChronoPost.UseCases.Users.Queries.RefreshJwtToken;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Moq;

namespace ChronoPost.UseCaseTests.User;

[TestFixture]
public sealed class RefreshJwtTokenHandlerTests
{
    private readonly RefreshTokenQueryHandler _handler;
    private readonly Mock<IDistributedCache> _cache = new();
    private readonly Mock<IPublisher> _publisher = new();
    private readonly Mock<IJwtService> _jwtService = new();
    private readonly Mock<IOptions<JwtOptions>> _options = new();

    public RefreshJwtTokenHandlerTests()
    {
        _handler = new RefreshTokenQueryHandler(_cache.Object, _jwtService.Object, _publisher.Object, _options.Object);
    }

    [Test]
    public async Task Handler_RefreshJwtTokenHandler()
    {
        var payloadMock = new JwtPayload(1, "string");
        var payloadContent = JsonSerializer.Serialize(payloadMock);

        _cache.Setup(_ => _.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(payloadContent));

        _jwtService.Setup(_ => _.GenerateAccessToken(payloadMock))
            .Returns(new JwtAccessToken(It.IsAny<string>(), It.IsAny<string>()));

        _options.Setup(_ => _.Value).Returns(new JwtOptions());

        var query = new RefreshJwtTokenQuery(It.IsAny<string>());
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Handler_ThrowsInvalidRefreshToken()
    {
        _cache.Setup(_ => _.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<byte[]>());

        Assert.CatchAsync<InvalidRefreshTokenException>(async () =>
        {
            await _handler.Handle(new RefreshJwtTokenQuery(It.IsAny<string>()), It.IsAny<CancellationToken>());
        });
    }
}