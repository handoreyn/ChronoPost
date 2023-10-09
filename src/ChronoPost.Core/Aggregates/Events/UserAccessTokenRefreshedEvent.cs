using ChronoPost.Core.Services.Jwt;
using MediatR;

namespace ChronoPost.Core.Aggregates.Events;

public sealed record UserAccessTokenRefreshedEvent(string PreviousRefreshToken,
    string RefreshToken, JwtPayload Payload, TimeSpan ExpirationInMinutes) : INotification;