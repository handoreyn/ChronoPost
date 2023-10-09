using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Specifications.User;
using ChronoPost.UseCases.Users.Queries.FindUserById;
using Moq;

namespace ChronoPost.UseCaseTests.User;

[TestFixture]
public class UserQueryTests
{
    private readonly Mock<IReadRepository<Core.Aggregates.User>> _readRepositoryMock = new();

    [Test]
    public async Task FindUserById_UserDoesExist()
    {
        _readRepositoryMock.Setup(o => o.FirstOrDefaultAsync(It.IsAny<UserByIdSpecification>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new Core.Aggregates.User
        {
            Id = 1,
            UserCredentials = new Core.ValueObjects.UserCredentialValueObject("handoreyn", "123456"),
            Email = "fatihgencaslan@yahoo.com",
            Status = Core.Enums.StatusType.Active,
            CreatedAt = DateTime.UtcNow
        });
        var handler = new FindUserByIdQueryHandler(_readRepositoryMock.Object);
        var result = await handler.Handle(new FindUserByIdQuery(It.IsAny<int>()), CancellationToken.None);
        Assert.That(result, Is.Not.Null);

    }

    [Test]
    public void FindUserById_UserDoesNotExist()
    {
        _readRepositoryMock.Setup(_ => _.FirstOrDefaultAsync(It.IsAny<UserByIdSpecification>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(default(Core.Aggregates.User));

        var handler = new FindUserByIdQueryHandler(_readRepositoryMock.Object);
        Assert.CatchAsync<UserDoesNotExistException>(async () =>
        {
            await handler.Handle(new FindUserByIdQuery(It.IsAny<int>()), CancellationToken.None);
        });
    }
}