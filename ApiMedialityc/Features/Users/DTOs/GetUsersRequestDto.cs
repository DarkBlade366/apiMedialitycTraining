using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.DTOs
{
    public class GetUsersRequestDto
    {
        public int Page { get; set; } = 1;           
        public int PageSize { get; set; } = 10;      
        public string? FullName { get; set; }
        public bool? IsActive { get; set; }
    }
}