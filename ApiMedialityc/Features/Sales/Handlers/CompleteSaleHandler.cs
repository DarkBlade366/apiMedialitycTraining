using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Enum;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class CompleteSaleHandler
        : ICommandHandler<CompleteSaleCommand, CompleteSaleResponseDto>
    {
        private readonly ApiDbContext _context;

        public CompleteSaleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CompleteSaleResponseDto> ExecuteAsync(CompleteSaleCommand command, CancellationToken ct)
        {
            var sale = await _context.Sales
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(s => s.Id == command.Request.Id    , ct);

            if (sale == null)
            {
                throw new ValidationException("Sale no existe");
            }

            if (sale.Status != SaleStatus.Pending)
            {
                throw new InvalidOperationException("Solo operaciones pendientes a ser completadas.");
            }

            sale.Status = SaleStatus.Completed;

            var vehicle = sale.Vehicle;
            vehicle.IsSold = true;

            _context.Sales.Update(sale);
            _context.Vehicles.Update(vehicle);

            await _context.SaveChangesAsync(ct);

            return new CompleteSaleResponseDto
            {
                Id = sale.Id,
                Status = sale.Status,
                Message = "Sale completado correctamente."
            };
        }
    }
}