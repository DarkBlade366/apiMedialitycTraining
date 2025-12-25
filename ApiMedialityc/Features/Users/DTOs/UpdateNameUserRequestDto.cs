using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class UpdateNameUserRequestDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}