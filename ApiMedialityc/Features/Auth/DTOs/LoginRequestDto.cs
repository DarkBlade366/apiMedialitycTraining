using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Auth.DTOs
{
    public class LoginRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}