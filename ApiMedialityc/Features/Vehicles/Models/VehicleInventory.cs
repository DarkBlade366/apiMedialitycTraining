using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.Enum;

namespace ApiMedialityc.Features.Vehicles.Models
{
    public class VehicleInventory
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public VehicleType Type { get; set; }
        public int AvailableQuantity { get; set; } = 0;
        public bool IsAvailable { get; private set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}