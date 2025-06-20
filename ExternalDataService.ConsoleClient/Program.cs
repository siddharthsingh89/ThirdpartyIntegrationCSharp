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


           
            Console.WriteLine("Checking ListAllusers");
            var userList = client.GetAllUsersAsync().GetAwaiter().GetResult();
            userList.ForEach(u => 
            {
                Console.WriteLine($"User ID: {u.Id}, Name: {u.FirstName} {u.LastName}, Email: {u.Email}");
            });

            IExternalDataService service = new ExternalDataService.Services.ThirdPartyUserService(client);
            Console.WriteLine("Checking GetUserById from service");
            var userFromService = service.GetUserByIdAsync(2).GetAwaiter().GetResult();
            Console.WriteLine($"User ID: {userFromService.Id}, Name: {userFromService.FirstName} {userFromService.LastName}, Email: {userFromService.Email}");
            var allUsersFromService = service.GetAllUsersAsync().GetAwaiter().GetResult();
            Console.WriteLine("Checking ListAllUsers from service");
            allUsersFromService.ToList().ForEach(u => 
            {
                Console.WriteLine($"User ID: {u.Id}, Name: {u.FirstName} {u.LastName}, Email: {u.Email}");
            });

        }
    }
}
