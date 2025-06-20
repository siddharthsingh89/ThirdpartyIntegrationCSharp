using ExternalDataService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Interfaces
{
    internal interface IExternalDataClient
    {
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<List<UserDto>> GetAllUsersAsync();
    }
}
