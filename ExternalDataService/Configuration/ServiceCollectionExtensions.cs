using ExternalDataService.Client;
using ExternalDataService.Interfaces;
using ExternalDataService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ExternalDataService.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServiceIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            // Fix: Use the correct method overload to bind the configuration section to the ApiSettings class.
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddHttpClient<IExternalDataClient, ExternalDataClient>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey);
            });

            // Register service
            services.AddScoped<IExternalDataService, ThirdPartyUserService>();

            return services;
        }
    }
}
