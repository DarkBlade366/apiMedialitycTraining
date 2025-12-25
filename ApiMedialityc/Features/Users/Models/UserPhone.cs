using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Users.Models
{
    public class UserPhone
    {
        public string Phone { get; set; }= string.Empty;
        public Guid Id  { get; set; }
        public Guid UserId { get; set; }

        // Navegación inversa obligatoria, EF la llenará
        public virtual User User { get; set; } = null!;
    }
}