using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.Enum;

namespace ApiMedialityc.Features.Vehicles.DTOs
{
    public class GetVehiclesRequestDto
    {
        public VehicleType? Type { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public bool? Availability { get; set; }
        public decimal? MinPrice { get; set; }   
        public decimal? MaxPrice { get; set; }   
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}