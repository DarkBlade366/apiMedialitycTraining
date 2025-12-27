using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Queries
{
    public class SalesReportQuery
        : ICommand<SalesReportResponseDto>
    {
        public SalesReportRequestDto Request { get; set; }
        public SalesReportQuery(SalesReportRequestDto request)
        {
            Request = request;
        }
    }
}