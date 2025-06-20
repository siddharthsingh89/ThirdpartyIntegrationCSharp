using ExternalDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Interfaces
{
    internal interface IExternalDataService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
