using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class SaleSummaryDto
    {
        public Guid SaleId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime SaleDate { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}