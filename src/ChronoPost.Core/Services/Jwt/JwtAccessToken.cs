namespace ChronoPost.Core.Services.Jwt;

public record JwtAccessToken(string AccessToken, string RefreshToken);