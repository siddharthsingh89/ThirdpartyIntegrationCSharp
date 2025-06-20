using ExternalDataService.Models;
using ExternalDataService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Services
{
    public class Converter
    {
        public static User GetUserEntityFromDto(UserDto dto)
        {
            if (dto == null) return null;
            return new User
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Avatar = dto.Avatar
            };
        }
    }
}
