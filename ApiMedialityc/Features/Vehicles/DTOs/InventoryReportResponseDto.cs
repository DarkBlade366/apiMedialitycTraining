using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Vehicles.DTOs
{
    public class InventoryReportResponseDto
    {
        public int TotalVehicles { get; set; }
        public int SoldVehicles { get; set; }
        public int AvailableVehicles { get; set; }
        public int PendingVehicles { get; set; }
        public Dictionary<string, int> VehiclesByType { get; set; } = new Dictionary<string, int>();
        public List<VehicleInventorySummaryDto> RecentMovements { get; set; } = new List<VehicleInventorySummaryDto>();
    }
}