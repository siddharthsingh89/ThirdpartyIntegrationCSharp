using ExternalDataService.Client;
using ExternalDataService.Interfaces;
namespace ExternalDataService.ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IExternalDataClient client = new ExternalDataClient();
            Console.WriteLine("Checking GetUserbyId");
            var user = client.GetUserByIdAsync(2).GetAwaiter().GetResult();
            Console.WriteLine($"User ID: {user?.Id}, Name: {user?.FirstName} {user?.LastName}, Email: {user?.Email}");
        }
    }
}
