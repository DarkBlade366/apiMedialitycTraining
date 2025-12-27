using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class CompleteSaleResponseDto
    {
        public Guid Id { get; set; }
        public SaleStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}