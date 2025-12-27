using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Sales.DTOs
{
    //pense hacer una clase generica con esto para usarlo pa todos los reportes como pagedresponse pero al 
    // final lo deje asi pq para mi proyecto no era necesario
    public class SalesReportResponseDto
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> SalesByStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> SalesByVehicleType { get; set; } = new Dictionary<string, int>();
        public List<SaleSummaryDto> RecentSales { get; set; } = new List<SaleSummaryDto>();
    }
}