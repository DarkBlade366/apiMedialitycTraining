using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMedialityc.Features.Sales.DTOs
{
    public class SalesReportRequestDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Status { get; set; }
        public Guid? VehicleId { get; set; }
        public Guid? UserId { get; set; }
    }
}