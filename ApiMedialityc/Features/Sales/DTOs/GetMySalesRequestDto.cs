using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class GetMySalesRequestDto
    {
        public SaleStatus? Status { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}