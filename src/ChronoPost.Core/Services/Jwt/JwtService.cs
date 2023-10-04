using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChronoPost.Core.Services.Jwt;

public sealed class JwtService : IJwtService
{
    private readonly IOptions<JwtOptions> _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options), "Jwt Options cannot be read");
    }


    public JwtAccessToken GenerateAccessToken(JwtPayload payload)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey));
        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: _options.Value.Issuer, audience: _options.Value.Issuer, claims: new Claim[] {
            new(JwtClaimNames.UserId.ToString(), payload.UserId.ToString()),
            new(JwtClaimNames.Username.ToString(), payload.Username)
            },

        expires: DateTime.Now.AddMinutes(_options.Value.ExpiresInMinutes), notBefore: DateTime.Now, signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var result = new JwtAccessToken(tokenHandler.WriteToken(token), GenerateRefreshToken());

        return result;
    }


    /// <summary>
    /// Generates base64 random string
    /// </summary>
    /// <returns>Base64String to use as Refresh Token</returns>
    private string GenerateRefreshToken()
    {
        var random = new byte[64];
        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(random);
        var token = Convert.ToBase64String(random);

        return token;
    }
}