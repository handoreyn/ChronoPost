using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Core.Exceptions;
using ChronoPost.Core.Specifications.User;

namespace ChronoPost.UseCases.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, int>
{
    private readonly IRepository<User> _repository;

    public UpdateUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FirstOrDefaultAsync(new UserByIdSpecification(request.UserId), cancellationToken)
            ?? throw new UserDoesNotExistException();

        user.Status = request.Status;
        user.Email = request.Email;
        user.UserCredentials = new Core.ValueObjects.UserCredentialValueObject(request.Username, user.UserCredentials.Password);

        return await _repository.SaveChangesAsync(cancellationToken);
    }
}
