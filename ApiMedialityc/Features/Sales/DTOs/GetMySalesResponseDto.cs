using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class GetMySalesResponseDto
    {
        public Guid SaleId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public decimal Price { get; set; }

        // Vehicle info
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}