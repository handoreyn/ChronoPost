using Ardalis.SharedKernel;
using ChronoPost.Core.Enums;
using ChronoPost.Core.Specifications.User;

namespace ChronoPost.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IRepository<Core.Aggregates.User> _repository;

    public CreateUserCommandHandler(IRepository<Core.Aggregates.User> repository)
    {
        _repository = repository;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userByUsernameOrEmailAddressSpecification = new UserByUserNameOrEmailSpecification(request.Username, request.Email);
        var user = await _repository.FirstOrDefaultAsync(userByUsernameOrEmailAddressSpecification);
        if (user is not null) throw new Exception("User does exist.");
        user = new Core.Aggregates.User
        {
            UserCredentials = new Core.ValueObjects.UserCredentialValueObject(request.Username, request.Password),
            Email = request.Email,
            Status = StatusType.Active,
            CreatedAt = DateTime.UtcNow,
        };

        await _repository.AddAsync(user, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var result = new CreateUserCommandResponse(user.Id, user.UserCredentials.Username, user.Email, user.Status);
        return result;
    }
}