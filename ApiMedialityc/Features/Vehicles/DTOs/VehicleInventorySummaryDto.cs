using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Vehicles.DTOs
{
    public class VehicleInventorySummaryDto
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}