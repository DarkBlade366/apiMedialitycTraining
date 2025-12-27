using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Vehicles.Commands;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using ApiMedialityc.Features.Sales.Enum;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class UpdateVehicleHandler
        : CommandHandler<UpdateVehicleCommand, UpdateVehicleResponseDto>
    {
        private readonly ApiDbContext _context;

        public UpdateVehicleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<UpdateVehicleResponseDto> ExecuteAsync(UpdateVehicleCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleInventory)
                .Include(v => v.Sales)
                .FirstOrDefaultAsync(v => v.Id == dto.Id, ct);

            if (vehicle == null)
            {
                throw new ValidationException("Vehículo no encontrado");
            }

            if (vehicle.Sales.Any(s => s.Status == SaleStatus.Pending))
            {
                throw new ValidationException("No se puede modificar un vehículo con una venta pendiente");
            }

            var plateExists = await _context.Vehicles
                .AnyAsync(v => v.Plate == dto.Plate && v.Id != dto.Id, ct);

            if (plateExists)
            {
                throw new ValidationException("Ya existe otro vehículo con esa placa");
            }

            // Ver si cambió la combinación de inventario
            var inventoryChanged =
                vehicle.Brand != dto.Brand ||
                vehicle.Model != dto.Model ||
                vehicle.Type != dto.Type;

            if (vehicle.IsSold)
            {
                throw new ValidationException("No se puede actualizar un vehículo ya vendido");
            }

            if (inventoryChanged)
            {
                // Reducir inventario anterior
                vehicle.VehicleInventory.AvailableQuantity--;

                var newInventory = await _context.VehicleInventories
                    .FirstOrDefaultAsync(v =>
                        v.Brand == dto.Brand &&
                        v.Model == dto.Model &&
                        v.Type == dto.Type, ct);

                if (newInventory == null)
                {
                    newInventory = new VehicleInventory
                    {
                        Id = Guid.NewGuid(),
                        Brand = dto.Brand,
                        Model = dto.Model,
                        Type = dto.Type,
                        AvailableQuantity = 1
                    };

                    _context.VehicleInventories.Add(newInventory);
                }
                else
                {
                    newInventory.AvailableQuantity++;
                }

                vehicle.VehicleInventoryId = newInventory.Id;
            }

            // Actualizar datos
            vehicle.Plate = dto.Plate;
            vehicle.Brand = dto.Brand;
            vehicle.Model = dto.Model;
            vehicle.Type = dto.Type;
            vehicle.Price = dto.Price;

            await _context.SaveChangesAsync(ct);

            return new UpdateVehicleResponseDto
            {
                Id = vehicle.Id,
                Message = "Vehículo actualizado correctamente"
            };
        }
    }
}
