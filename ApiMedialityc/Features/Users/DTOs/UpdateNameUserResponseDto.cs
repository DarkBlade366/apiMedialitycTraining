using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class UpdateNameUserResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}