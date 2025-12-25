using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class ChangeStatusUserRequestDto
    {
        public bool IsActive { get; set; }
        public Guid Id { get; set; }
    }
}