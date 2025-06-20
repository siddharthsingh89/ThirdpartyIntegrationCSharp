using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExternalDataService.Models.Dto
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }


        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

    }

    public class UserResponseDto
    {
        [JsonPropertyName("data")]
        public UserDto Data { get; set; }

        [JsonPropertyName("support")]
        public Support Support { get; set; }
    }

    public class Support
    {     
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }      

    }
}
