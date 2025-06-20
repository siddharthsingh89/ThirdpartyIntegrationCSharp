using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Services
{
    internal class ThirdPartyUserService : IExternalDataService
    {
        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
