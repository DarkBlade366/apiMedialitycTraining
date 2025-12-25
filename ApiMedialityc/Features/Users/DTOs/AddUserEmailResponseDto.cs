using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class AddUserEmailResponseDto
    {
        public Guid UserId { get; set; } 
        public Guid EmailId { get; set; } 
        public string Message { get; set; } = string.Empty;
    }
}