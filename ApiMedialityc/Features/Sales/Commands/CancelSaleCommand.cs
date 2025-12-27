using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Commands
{
    public class CancelSaleCommand
        : ICommand<CancelSaleResponseDto>
    {
        public CancelSaleRequestDto Request { get; set; }
        public Guid UserId { get; set; } // Para validar si es dueño de la venta

        public CancelSaleCommand(CancelSaleRequestDto request, Guid userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}