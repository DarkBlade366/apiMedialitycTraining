using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class DeleteUserPhoneResponseDto
    {
        public Guid Id { get; set; } 
        public string Phone { get; set; } = string.Empty; 
        public string Message { get; set; } = string.Empty;
    }
}