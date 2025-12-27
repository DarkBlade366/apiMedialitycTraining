using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Commands
{
    public class CompleteSaleCommand
        : ICommand<CompleteSaleResponseDto>
    {
        public CompleteSaleRequestDto Request { get; set; }
        public CompleteSaleCommand(CompleteSaleRequestDto request)
        {
            Request = request;
        }
    }
}