using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class CreateSaleResponseDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}