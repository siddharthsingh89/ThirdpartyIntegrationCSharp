using ExternalDataService.Client;
using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ExternalDataService.ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

        
            var services = new ServiceCollection();
            services.AddDataServiceIntegration(configuration);
            var provider = services.BuildServiceProvider();

         
            var userService = provider.GetRequiredService<IExternalDataService>();

         
            var user = await userService.GetUserByIdAsync(2);

            if (user != null)
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                Console.WriteLine($"Email: {user.Email}");
            }
            else
            {
                Console.WriteLine("User not found.");
            }

        }
    }
}
