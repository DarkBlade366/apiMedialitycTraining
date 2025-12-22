using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.Models
{
    public class UserPhone
    {
        public string phone { get; set; }= string.Empty;
        public Guid id  { get; set; }
        public Guid userId { get; set; }
    }
}