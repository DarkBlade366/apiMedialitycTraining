using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ApiMedialityc.Features.Users.Enum;
using ApiMedialityc.Features.Users.DTOs;

namespace ApiMedialityc.Features.Users.Models
{
    public class User
    {
        public string FullName { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set;}
        public Role Role { get; set; }

        public ICollection<UserEmail> Emails { get; set;} = new List<UserEmail>();
        public ICollection<UserPhone> Phones { get; set;} = new List<UserPhone>();
    }
}