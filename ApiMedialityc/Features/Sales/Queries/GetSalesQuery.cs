using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

public class GetSalesQuery 
    : ICommand<PagedResponse<GetSalesResponseDto>>
{
    public GetSalesRequestDto Request { get; set; }

    public GetSalesQuery(GetSalesRequestDto request)
    {
        Request = request;
    }
}
