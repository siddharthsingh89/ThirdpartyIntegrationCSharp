using ExternalDataService.Client;
using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using Spectre.Console;

namespace ExternalDataService.ConsoleClient
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var service = Initializer.InitializeService();

            var rootCommand = new RootCommand("External user Data management CLI");
         
            var getCommand = new Command("get", "Get user by ID");
            var idArgument = new Argument<int>("id", "User ID");
            getCommand.AddArgument(idArgument);
            getCommand.SetHandler(async (id) =>
            {
                Console.WriteLine($"Fetching user with ID: {id}");
                var user = await service.GetUserByIdAsync(id);
                PrintHelper.PrintUserData(user);
            }, idArgument);

            
            var listCommand = new Command("list", "List all users");
            listCommand.SetHandler(async () =>
            {
                Console.WriteLine("Listing all users...");
                var userList = await service.GetAllUsersAsync();
                PrintHelper.PrintUserList(userList);
            });

            
            var usersCommand = new Command("users", "User-related commands");
            usersCommand.AddCommand(getCommand);
            usersCommand.AddCommand(listCommand);

            rootCommand.AddCommand(usersCommand);

            return await rootCommand.InvokeAsync(args);
        }
   
    }
}
