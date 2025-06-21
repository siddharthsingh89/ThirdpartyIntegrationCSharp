using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.ConsoleClient
{
    internal class Initializer
    {
        internal static IExternalDataService InitializeService()
        {
            var configuration = BuildConfiguration();
            var userService = BuildUserService(configuration);
            return userService;
        }
        internal static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        internal static IExternalDataService BuildUserService(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            services.AddDataServiceIntegration(configuration);
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IExternalDataService>();
        }
    }
}
