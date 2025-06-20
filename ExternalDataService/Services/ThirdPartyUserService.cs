using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        public ThirdPartyUserService(IExternalDataClient externalDataClient, 
            IMemoryCache cache,
            IOptions<CacheSettings> cacheSettings)
        {
            _externalDataClient =externalDataClient ?? throw new ArgumentNullException(nameof(externalDataClient));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheSettings = cacheSettings.Value ?? throw new ArgumentNullException(nameof(cacheSettings));
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            string cacheKey = "all_users";

            if (_cache.TryGetValue(cacheKey, out List<User> cachedUsers))
                return cachedUsers;

            var dtos = await _externalDataClient.GetAllUsersAsync();
            var users = dtos.Select(Converter.GetUserEntityFromDto).ToList();

            _cache.Set(cacheKey, users, TimeSpan.FromMinutes(_cacheSettings.AllUserCacheMinutes));

            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {

            string cacheKey = $"user_{userId}";

            //get from cache first
            if (_cache.TryGetValue(cacheKey, out User cachedUser))
                return cachedUser;

            var dto = await _externalDataClient.GetUserByIdAsync(userId);
            if (dto == null)
            {
                return null;
            }

            var user = Converter.GetUserEntityFromDto(dto);
           
            _cache.Set(cacheKey, user, TimeSpan.FromMinutes(_cacheSettings.UserCacheMinutes));

            return user;
        }
    }
}
