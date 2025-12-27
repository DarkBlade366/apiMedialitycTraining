using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Vehicles.DTOs
{
    public class CreateVehicleResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}