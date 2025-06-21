using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ThirdPartyUserService> _logger;
        public ThirdPartyUserService(IExternalDataClient externalDataClient,
            IMemoryCache cache,
            IOptions<CacheSettings> cacheSettings,
            ILogger<ThirdPartyUserService> logger)
        {
            _externalDataClient =externalDataClient ?? throw new ArgumentNullException(nameof(externalDataClient));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheSettings = cacheSettings.Value ?? throw new ArgumentNullException(nameof(cacheSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            string cacheKey = "all_users";

            if (_cache.TryGetValue(cacheKey, out List<User> cachedUsers))
            {
                _logger.LogInformation("all_users cache key found. Returning data from cache");
                return cachedUsers;
            }

            _logger.LogInformation("Cache miss. Calling GetAllUsersAsync() using client");
            var dtos = await _externalDataClient.GetAllUsersAsync();
            var users = dtos.Select(Converter.GetUserEntityFromDto).ToList();
            _logger.LogInformation($"Data received and Converted. Total count= {users?.Count}");
            _cache.Set(cacheKey, users, TimeSpan.FromMinutes(_cacheSettings.AllUserCacheMinutes));
            _logger.LogInformation($"Cache Key {cacheKey} set with  {_cacheSettings.AllUserCacheMinutes} duration");
            return users;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {

            string cacheKey = $"user_{userId}";

            //get from cache first
            if (_cache.TryGetValue(cacheKey, out User cachedUser))
            {
                _logger.LogInformation("Returning data from cache");
                return cachedUser;
            }
                

            var dto = await _externalDataClient.GetUserByIdAsync(userId);
            if (dto == null)
            {
                return null;
            }

            var user = Converter.GetUserEntityFromDto(dto);
           
            _cache.Set(cacheKey, user, TimeSpan.FromMinutes(_cacheSettings.UserCacheMinutes));
            _logger.LogInformation($"Set cache key {cacheKey} in cache with {_cacheSettings.UserCacheMinutes} expiration");
            return user;
        }
    }
}
