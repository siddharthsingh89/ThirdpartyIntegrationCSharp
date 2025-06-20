using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExternalDataService.Models.Dto
{
    public class UserPageDto
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int Per_Page { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int Total_Pages { get; set; }

        [JsonPropertyName("data")]
        public List<UserDto> Data { get; set; }

        [JsonPropertyName("support")]   
        public Support Support { get; set; }

    }
}
