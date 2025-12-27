using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.Enum;

namespace ApiMedialityc.Features.Vehicles.DTOs
{
    public class GetVehiclesResponseDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; } 
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public VehicleType Type { get; set; } = VehicleType.Other;
        public bool Availability { get; set; } = false;
        public int AvailableQuantity { get; set; }

        public bool IsSold { get; set; } = false;
    }
}