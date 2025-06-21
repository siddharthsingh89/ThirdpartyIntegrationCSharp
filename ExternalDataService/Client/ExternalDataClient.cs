using ExternalDataService.Interfaces;
using ExternalDataService.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExternalDataService.Client
{
    public class ExternalDataClient : IExternalDataClient
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<ExternalDataClient> _logger;   

        public ExternalDataClient(HttpClient httpClient, ILogger<ExternalDataClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var allUsers = new List<UserDto>();
            int page = 1, totalPages;
            _logger.LogInformation("Starting to fetch all users from external service.");
            try
            {
                do
                {
                    _logger.LogInformation($"GET to endpoint {_httpClient.BaseAddress}/users?page={page}");
                    var response = await _httpClient.GetAsync($"users?page={page}");
                    response.EnsureSuccessStatusCode();
                    _logger.LogInformation($"GET Request Success and response received. Starting Deserialization");
                    var content = await response.Content.ReadAsStringAsync();
                    UserPageDto? data = null;
                    try
                    {
                        data = JsonSerializer.Deserialize<UserPageDto>(content);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError($"Deserialization error on page {page}: {ex.Message}");
                        break;
                    }

                    if (data?.Data != null)
                        allUsers.AddRange(data.Data);

                    totalPages = data?.Total_Pages ?? 0;
                    page++;
                } while (page <= totalPages);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError($"Request timed out: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error: {ex.Message}");
            }

            return allUsers;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"Starting to Get user detail with {userId} from external service.");
                var response = await _httpClient.GetAsync($"users/{userId}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogError($" UserId {userId} not found in external service.");
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"GET Request Success and response received. Starting Deserialization");
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    UserResponseDto? userObjectFromResponse = null;
                    try
                    {
                        userObjectFromResponse = JsonSerializer.Deserialize<UserResponseDto>(jsonResponse);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError($"Deserialization error for user {userId}: {ex.Message}");
                        return null;
                    }            
                    return userObjectFromResponse?.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError($"Request timed out for user {userId}: {ex.Message}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error for user {userId}: {ex.Message}");
                return null;
            }
        }
    }
}
