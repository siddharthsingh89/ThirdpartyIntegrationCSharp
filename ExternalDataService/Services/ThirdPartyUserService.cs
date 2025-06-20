using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Services
{
    public class ThirdPartyUserService : IExternalDataService
    {
        private readonly IExternalDataClient _externalDataClient;
        public ThirdPartyUserService(IExternalDataClient externalDataClient)
        {
            _externalDataClient = externalDataClient;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var userList = await _externalDataClient.GetAllUsersAsync();
            return userList.Select(Converter.GetUserEntityFromDto);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var dtoResponse = await _externalDataClient.GetUserByIdAsync(userId);
            if (dtoResponse == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            return Converter.GetUserEntityFromDto(dtoResponse);

        }
    }
}
