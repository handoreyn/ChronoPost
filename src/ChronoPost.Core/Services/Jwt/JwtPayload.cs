using System.Security.Claims;

namespace ChronoPost.Core.Services.Jwt;

public record JwtPayload(long UserId, string Username);

public static class JwtClaimExtension
{
    public static JwtPayload ToUserClaims(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.Claims.FirstOrDefault(r => r.Type.Equals(JwtClaimNames.UserId.ToString()))?.Value;
        int.TryParse(userId, out var id);

        return new JwtPayload(id, claimsPrincipal.Claims.FirstOrDefault(r => r.Type.Equals(JwtClaimNames.Username.ToString()))?.Value);
    }
}