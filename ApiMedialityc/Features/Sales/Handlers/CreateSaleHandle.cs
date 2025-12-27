using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FastEndpoints;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class CreateSaleHandler 
        : ICommandHandler<CreateSaleCommand, CreateSaleResponseDto>
    {
        private readonly ApiDbContext _context;

        public CreateSaleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateSaleResponseDto> ExecuteAsync(CreateSaleCommand command, CancellationToken ct)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleInventory)
                .FirstOrDefaultAsync(v => v.Id == command.Request.VehicleId, ct);

            if (vehicle == null)
            {
                throw new ValidationException("El vehículo no existe.");
            }

            if (vehicle.IsSold)
            {
                throw new ValidationException("Vehículo ya vendido.");
            }

            if (vehicle.VehicleInventory.AvailableQuantity <= 0)
            {
                throw new ValidationException("No hay vehículos disponibles en inventario.");
            }

            // Crear venta
            var sale = new Sale
            {
                VehicleId = vehicle.Id,
                UserId = command.UserId,
                Price = vehicle.Price,
                SaleDate = DateTime.UtcNow,
                Status = SaleStatus.Pending
            };

            // Actualizar inventario
            vehicle.VehicleInventory.AvailableQuantity--;

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync(ct);

            return new CreateSaleResponseDto
            {
                Id = sale.Id,
                VehicleId = vehicle.Id,
                UserId = command.UserId,
                Price = vehicle.Price,
                VehicleBrand = vehicle.Brand,
                VehicleModel = vehicle.Model,
                SaleDate = sale.SaleDate,
                Status = sale.Status.ToString()
            };
        }
    }
}
