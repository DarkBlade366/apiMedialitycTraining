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
    public class CancelSaleHandler : ICommandHandler<CancelSaleCommand, CancelSaleResponseDto>
    {
        private readonly ApiDbContext _context;

        public CancelSaleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CancelSaleResponseDto> ExecuteAsync(CancelSaleCommand command, CancellationToken ct)
        {
            var sale = await _context.Sales
                .Include(s => s.Vehicle)
                .ThenInclude(v => v.VehicleInventory)
                .FirstOrDefaultAsync(s => s.Id == command.Request.Id, ct);

            if (sale == null)
            {
                throw new ValidationException("Sale no existe");
            }

            // Si la venta está completada, no se puede cancelar
            if (sale.Status == SaleStatus.Completed)
            {
                throw new InvalidOperationException("No se puede cancelar una venta completada.");
            }

            // Si es usuario normal, solo puede cancelar su propia venta
            if (sale.UserId != command.UserId)
            {
                throw new UnauthorizedAccessException("No puedes cancelar ventas de otros usuarios.");
            }

            sale.Status = SaleStatus.Cancelled;

            // Incrementa inventario (el if esta por si acaso, pero siempre deberia ser distinto de null)
            if (sale.Vehicle.VehicleInventory != null)
            {
                sale.Vehicle.VehicleInventory.AvailableQuantity++;
                _context.VehicleInventories.Update(sale.Vehicle.VehicleInventory);
            }

            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(ct);

            return new CancelSaleResponseDto
            {
                Id = sale.Id,
                Status = sale.Status,
                Message = "Venta cancelada correctamente."
            };
        }
    }
}
