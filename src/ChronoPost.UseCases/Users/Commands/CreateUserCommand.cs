using Ardalis.SharedKernel;

namespace ChronoPost.UseCases.Users.Commands;

public sealed record CreateUserCommand(string Username, string Password, string Email) : ICommand<CreateUserCommandResponse>;