using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Vehicles.Commands;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class DeleteVehicleHandler
        : CommandHandler<DeleteVehicleCommand, DeleteVehicleResponseDto>
    {
        private readonly ApiDbContext _context;

        public DeleteVehicleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<DeleteVehicleResponseDto> ExecuteAsync(DeleteVehicleCommand c, CancellationToken ct)
        {
            var id = c.Request.Id;

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleInventory)
                .Include(v => v.Sales)
                .FirstOrDefaultAsync(v => v.Id == id, ct);

            if (vehicle == null)
            {
                throw new ValidationException("Vehículo no encontrado");
            }

            if (vehicle.Sales.Any(s => s.Status == SaleStatus.Pending))
            {
                throw new ValidationException("No se puede eliminar un vehículo con una venta pendiente");
            }

            //Esta validacion nunca deberia de entrar, como tal es es una validación de consistencia, no de negocio.
            if (vehicle.VehicleInventory.AvailableQuantity <= 0)
            {
                throw new ValidationException("El inventario del vehículo ya está en 0, no se puede eliminar");
            }

            if (vehicle.IsSold)
            {
                throw new ValidationException("No se puede eliminar un vehículo que ya ha sido vendido");
            }

            // Restar del inventario
            vehicle.VehicleInventory.AvailableQuantity--;

            // Eliminar vehículo
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(ct);

            return new DeleteVehicleResponseDto
            {
                Id = id,
                Message = "Vehículo eliminado correctamente"
            };
        }
    }
}
