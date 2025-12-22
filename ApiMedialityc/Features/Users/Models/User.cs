using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ApiMedialityc.Features.Users.Enum;

namespace ApiMedialityc.Features.Users.Models
{
    public class User
    {
        public string fullName { get; set; } = string.Empty;
        public Guid id { get; set; }
        public string password { get; set; } = string.Empty;
        public bool isActive { get; set;}
        public Rol rol;

        public ICollection<UserEmail> emails = new List<UserEmail>();
        public ICollection<UserPhone> phones = new List<UserPhone>();
    }
}