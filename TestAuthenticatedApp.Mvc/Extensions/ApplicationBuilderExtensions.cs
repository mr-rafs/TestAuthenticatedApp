using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using TestAuthenticatedApp.SharedLayer.Settings;

namespace TestAuthenticatedApp.Mvc.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddKeycloakSettings(this WebApplicationBuilder builder)
        {
            var keycloakSettings = builder.Configuration.GetSection("Keycloak");

            builder.Services.Configure<KeycloakSettings>(keycloakSettings);
        }

        public static void AddKeycloakAuthorization(this WebApplicationBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;

            builder.Services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://{keycloakHost}:{keycloakPort}/realms/{yourDevelopRealm}";
                    options.SaveToken = false;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://{keycloakHost}:{keycloakPort}/realms/{yourProductionRealm}"
                    };
                });
        }
    }
}
