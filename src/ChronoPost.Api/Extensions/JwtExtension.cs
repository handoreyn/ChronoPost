using ChronoPost.Core.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ChronoPost.Api.Extensions;

public static class JwtExtension
{
    /// <summary>
    /// Generates JWT Access token by given JWT payload
    /// </summary>
    /// <param name="payload">Represents JWT Token's payload</param>
    /// <returns>JWTAccessToken</returns>
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtService, JwtService>();
    }
}