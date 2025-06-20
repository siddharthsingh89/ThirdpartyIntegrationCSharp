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

        public ExternalDataClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://reqres.in/api/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {         
            
            var response = await _httpClient.GetAsync($"users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var userObjectFromResponse = JsonSerializer.Deserialize<UserResponseDto>(jsonResponse);
                Console.WriteLine($"Response: {jsonResponse}");
                Console.WriteLine($"Response obj: {userObjectFromResponse}");
                return userObjectFromResponse?.Data;

            }
            else
            {
                // Handle error response, e.g., log it or throw an exception
                return null;
            }
        }
    }
}
