using ExternalDataService.Interfaces;
using ExternalDataService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Client
{
    internal class ExternalDataClient : IExternalDataClient
    {
        public Task<List<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
