using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Enum;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class GetUsersResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Role Role;
        public List<UserEmailDto> Emails { get; set; } = new List<UserEmailDto>();
        public List<UserPhoneDto> Phones { get; set; } = new List<UserPhoneDto>();
    }
}