namespace ChronoPost.Core.Services.Jwt;

public class JwtOptions
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int ExpiresInMinutes { get; init; }
    public int RefreshTokenExpiresInMinutes { get; set; }
}
public enum JwtClaimNames
{
    UserId,
    Username
}