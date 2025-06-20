using ExternalDataService.Client;
using ExternalDataService.Interfaces;
using ExternalDataService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using System;
using System.Net.Http;

namespace ExternalDataService.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServiceIntegration(this IServiceCollection services, IConfiguration configuration)
        {         
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.AddMemoryCache();
            services.AddHttpClient<IExternalDataClient, ExternalDataClient>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey);
            }).AddPolicyHandler(RetryConfiguration.GetRetryPolicy())
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));


            services.AddScoped<IExternalDataService, ThirdPartyUserService>();

            return services;
        }
    }
}
