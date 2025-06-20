using ExternalDataService.Interfaces;
using ExternalDataService.Models.Dto;
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

        public ExternalDataClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var allUsers = new List<UserDto>();
            int page = 1, totalPages;

            try
            {
                do
                {
                    var response = await _httpClient.GetAsync($"users?page={page}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    UserPageDto? data = null;
                    try
                    {
                        data = JsonSerializer.Deserialize<UserPageDto>(content);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Deserialization error on page {page}: {ex.Message}");
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
                Console.WriteLine($"Request timed out: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
            }

            return allUsers;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"users/{userId}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // User not found
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    UserResponseDto? userObjectFromResponse = null;
                    try
                    {
                        userObjectFromResponse = JsonSerializer.Deserialize<UserResponseDto>(jsonResponse);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Deserialization error for user {userId}: {ex.Message}");
                        return null;
                    }
                    Console.WriteLine($"Response: {jsonResponse}");
                    Console.WriteLine($"Response obj: {userObjectFromResponse}");
                    return userObjectFromResponse?.Data;
                }
                else
                {
                    return null;
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Request timed out for user {userId}: {ex.Message}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error for user {userId}: {ex.Message}");
                return null;
            }
        }
    }
}
