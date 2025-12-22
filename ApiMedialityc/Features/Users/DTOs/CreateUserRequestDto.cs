using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class CreateUserRequestDto
    {
        public string fullName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public List<UserEmailDto> emails { get; set; } = new List<UserEmailDto>();
        public List<UserPhoneDto> phones { get; set; } = new List<UserPhoneDto>();
        
    }
}