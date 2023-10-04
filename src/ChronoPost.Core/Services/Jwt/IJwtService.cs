namespace ChronoPost.Core.Services.Jwt;

/// <summary>
/// Defines <c>IJWTService</c> interface to implement JWT Token services.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates JWT Access Token by given payload
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    JwtAccessToken GenerateAccessToken(JwtPayload payload);
}