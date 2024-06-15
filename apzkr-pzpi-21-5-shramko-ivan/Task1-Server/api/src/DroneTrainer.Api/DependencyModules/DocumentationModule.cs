using Microsoft.OpenApi.Models;

namespace DroneTrainer.Api.DependencyModules;

internal static class DocumentationModule
{
    private const string _documentName = "api";
    private const string _documentTitle = "Requr Portal Service";
    private const string _documentVersion = "v1";
    private const string _authScheme = "Bearer";
    private const string _authHeaderName = "Authorization";
    private const string _accessTokenFormat = "JWT";

    public static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(_authScheme, new OpenApiSecurityScheme
            {
                Description = @"Use an access token(JWT) to access endpoints that require authenticated users.",
                Name = _authHeaderName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = _accessTokenFormat,
                Scheme = _authScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = _authScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseDocumentation(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
        applicationBuilder.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.DocumentTitle = _documentTitle;

            options.SwaggerEndpoint($"/swagger/{_documentName}/swagger.json", _documentName);
        });

        return applicationBuilder;
    }
}
