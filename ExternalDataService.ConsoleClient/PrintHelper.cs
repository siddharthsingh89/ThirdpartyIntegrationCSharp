using ExternalDataService.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.ConsoleClient
{
    internal static class PrintHelper
    {
        internal static void PrintUserData(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            var table = new Table();

            AnsiConsole.MarkupLine("[green]User Details:[/]");
            AnsiConsole.Write(new Table()
                .AddColumn("Id")
                .AddColumn("Name")
                .AddColumn("Email")
                .AddRow(user.Id.ToString(), $"{user.FirstName} {user.LastName}", user.Email));

        }

        internal static void PrintUserList(IEnumerable<User> userList)
        {
            if (userList == null || userList.Count() == 0)
            {
                Console.WriteLine("No user data.");
                return;
            }
            AnsiConsole.MarkupLine("[green]User List:[/]");
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Email");


            foreach (User user in userList)
            {
                if (user == null) continue;
                table.AddRow(user.Id.ToString(), $"{user.FirstName} {user.LastName}", user.Email);

            }

            table.Border(TableBorder.Rounded);
            table.Centered();

            AnsiConsole.Write(table);
        }
    }
}
