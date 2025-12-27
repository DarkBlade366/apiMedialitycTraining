using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Queries
{
    public class GetMySalesQuery
        : ICommand<PagedResponse<GetMySalesResponseDto>>
    {
        public Guid UserId { get; }
        public GetMySalesRequestDto Request { get; }

        public GetMySalesQuery(Guid userId, GetMySalesRequestDto request)
        {
            UserId = userId;
            Request = request;
        }
    }
}