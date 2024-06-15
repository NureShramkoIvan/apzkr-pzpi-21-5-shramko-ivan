using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DroneTrainer.Api.DependencyModules;

internal static class AuthenticationModule
{
    internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfigurationSection jwtSettings)
    {
        services.ConfigureJwtAuth(jwtSettings);
        return services;
    }

    private static IServiceCollection ConfigureJwtAuth(this IServiceCollection services, IConfigurationSection jwtSettings)
    {
        var issuer = jwtSettings["Issuer"];
        var secretKey = jwtSettings["SecretKey"];

        var tokenValidationParamters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            LifetimeValidator = (before, expires, _, _) => expires.Value > DateTime.UtcNow
        };

        services
            .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = tokenValidationParamters;
            options.RequireHttpsMetadata = true;
        });

        return services;
    }
}
