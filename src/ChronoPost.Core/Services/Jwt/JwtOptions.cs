namespace ChronoPost.Core.Services.Jwt;

public class JwtOptions
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int ExpiresInMinutes { get; init; }
}
public enum JwtClaimNames
{
    UserId,
    Username
}