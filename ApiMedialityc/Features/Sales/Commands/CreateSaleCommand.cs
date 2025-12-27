using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Commands
{
    public class CreateSaleCommand
        : ICommand<CreateSaleResponseDto>
    {
        public CreateSaleRequestDto Request { get; set; }
        public Guid UserId { get; set; }

        public CreateSaleCommand(CreateSaleRequestDto request, Guid userId)
        {
            Request = request;
            UserId = userId;
        } 
    }
}