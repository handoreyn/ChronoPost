using ChronoPost.Core.Enums;

namespace ChronoPost.UseCases.Users.Commands;

public sealed record CreateUserCommandResponse(int UserId, string Username, string Email, StatusType Status);