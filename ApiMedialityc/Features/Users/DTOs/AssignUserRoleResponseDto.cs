using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class AssignUserRoleResponseDto
    {
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}