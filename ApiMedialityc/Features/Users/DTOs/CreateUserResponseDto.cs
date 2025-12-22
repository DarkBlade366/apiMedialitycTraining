using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class CreateUserResponseDto
    {
        public Guid id { get; set; }
        public string message { get; set; } = string.Empty;
    }
}