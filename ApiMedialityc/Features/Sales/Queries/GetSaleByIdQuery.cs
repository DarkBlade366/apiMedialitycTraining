using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Queries
{
    public class GetSaleByIdQuery 
        : ICommand<GetSaleByIdResponseDto>
    {
        public GetSaleByIdRequestDto Request { get; set; }
        public Guid CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public GetSaleByIdQuery(GetSaleByIdRequestDto request, Guid currentUserId, bool isAdmin)
        {
            Request = request;
            CurrentUserId = currentUserId;
            IsAdmin = isAdmin;
        }
    }
}
