using System.Security.Authentication;
using Ardalis.SharedKernel;
using ChronoPost.Core.Services.Jwt;
using ChronoPost.Core.Specifications.User;
using ChronoPost.UseCases.Users.Queries.GenerateJwtToken;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Moq;

namespace ChronoPost.UseCaseTests.User;

[TestFixture]
public sealed class GenerateJwtTokenHandlerTests
{
    private GenerateJwtTokenHandler _handler;

    public GenerateJwtTokenHandlerTests()
    {
        _handler = new GenerateJwtTokenHandler(_readRepository.Object, _jwtService.Object,
            _cache.Object, _options.Object, _publisher.Object);
    }

    private readonly Mock<IReadRepository<Core.Aggregates.User>> _readRepository = new();
    private readonly Mock<IJwtService> _jwtService = new();
    private readonly Mock<IDistributedCache> _cache = new();
    private readonly Mock<IOptions<JwtOptions>> _options = new();
    private readonly Mock<IPublisher> _publisher = new();

    [Test]
    public void Handler_ThrowsInvalidOperationException()
    {
        _readRepository.Setup(_ => _.FirstOrDefaultAsync(It.IsAny<UserByUserCredentialSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(Core.Aggregates.User));

        Assert.CatchAsync<InvalidCredentialException>(async () =>
        {
            await _handler.Handle(new GenerateJwtTokenQuery(It.IsAny<string>(), It.IsAny<string>()), It.IsAny<CancellationToken>());
        });
    }

    [Test]
    public async Task Handler_GeneratesToken()
    {
        _readRepository.Setup(_ => _.FirstOrDefaultAsync(It.IsAny<UserByUserCredentialSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Core.Aggregates.User
            {
                Id = 1,
                UserCredentials = new Core.ValueObjects.UserCredentialValueObject("handoreyn", "123456"),
                Email = "",
                Status = Core.Enums.StatusType.Active,
                CreatedAt = DateTime.UtcNow
            });

        _jwtService.Setup(_ => _.GenerateAccessToken(It.IsAny<JwtPayload>()))
            .Returns(new JwtAccessToken("access-token", "refresh-token"));

        _options.Setup(_ => _.Value).Returns(new JwtOptions
        {
            Audience = "",
            Issuer = "",
            SecretKey = "",
            ExpiresInMinutes = 1,
            RefreshTokenExpiresInMinutes = 1
        });

        _handler = new GenerateJwtTokenHandler(_readRepository.Object, _jwtService.Object,
            _cache.Object, _options.Object, _publisher.Object);

        var token = await _handler.Handle(new GenerateJwtTokenQuery(It.IsAny<string>(), It.IsAny<string>()), It.IsAny<CancellationToken>());

        Assert.That(token, Is.Not.Null);
    }
}