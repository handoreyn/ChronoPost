
using Ardalis.SharedKernel;
using ChronoPost.Core.Enums;

namespace ChronoPost.UseCases.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(int UserId, string Username, string Email, StatusType Status) : ICommand<int>;