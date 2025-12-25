using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class AddUserEmailRequestDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}