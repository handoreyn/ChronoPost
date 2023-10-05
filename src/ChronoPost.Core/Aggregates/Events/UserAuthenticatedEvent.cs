using Ardalis.SharedKernel;
using ChronoPost.Core.Services.Jwt;

namespace ChronoPost.Core.Aggregates.Events;

public sealed class UserAuthenticatedEvent : DomainEventBase
{
    public int UserId { get; init; }
    public string RefreshToken { get; init; }
    public TimeSpan ExpirationInMinutes { get; init; }
    public JwtPayload Payload { get; init; }
    public UserAuthenticatedEvent(int userId, string refreshToken, TimeSpan expirationInMinutes, JwtPayload payload)
    {
        UserId = userId;
        RefreshToken = refreshToken;
        ExpirationInMinutes = expirationInMinutes;
        Payload = payload;
    }
}