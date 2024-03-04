using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAuthenticatedApp.AppLayer.Interfaces;
using TestAuthenticatedApp.AppLayer.Services;

namespace TestAuthenticatedApp.AppLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IKeycloakTokenService, KeycloakTokenService>();



            return services;
        }
    }
}
