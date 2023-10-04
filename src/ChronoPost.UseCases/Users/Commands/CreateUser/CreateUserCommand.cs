using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Username, string Password, string Email) : ICommand<CreateUserCommandResponse>;