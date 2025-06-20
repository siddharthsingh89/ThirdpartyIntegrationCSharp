using ExternalDataService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Interfaces
{
    public interface IExternalDataClient
    {
        public Task<UserDto?> GetUserByIdAsync(int userId);
        public Task<List<UserDto>> GetAllUsersAsync();
    }
}
