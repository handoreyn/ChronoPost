using Ardalis.SharedKernel;
using ChronoPost.Core.Exceptions;
using ChronoPost.UseCases.Users.FindUserById;
using Moq;

namespace ChronoPost.UseCaseTests.User;

[TestFixture]
public class UserQueryTests
{
    private IReadRepository<Core.Aggregates.User> _readRepository;

    [SetUp]
    public void FindUserById_UserDoesExist_Setup()
    {
        var mock = new Mock<IReadRepository<Core.Aggregates.User>>();
        var user = new Core.Aggregates.User("", "", "")
        {
            Id = 1
        };

        mock.Setup(o => o.GetByIdAsync(1, CancellationToken.None))
            .ReturnsAsync(user);

        _readRepository = mock.Object;
    }

    [Test]
    public async Task FindUserById_UserDoesExist()
    {
        var handler = new FindUserByIdQueryHandler(_readRepository);
        var result = await handler.Handle(new FindUserByIdQuery(1), CancellationToken.None);
        Assert.That(result, Is.Not.Null);

    }

    [TestCase(0)]
    public void FindUserById_UserDoesNotExist(int userId)
    {
        var handler = new FindUserByIdQueryHandler(_readRepository);
        Assert.CatchAsync<UserDoesNotExistException>(async () =>
        {
            await handler.Handle(new FindUserByIdQuery(userId), CancellationToken.None);
        });
    }
}