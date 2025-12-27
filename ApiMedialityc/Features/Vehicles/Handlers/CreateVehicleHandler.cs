using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Vehicles.Commands;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Enum;
using ApiMedialityc.Features.Vehicles.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class CreateVehicleHandler
        : CommandHandler<CreateVehicleCommand, CreateVehicleResponseDto>
    {
        private readonly ApiDbContext _context;

        public CreateVehicleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<CreateVehicleResponseDto> ExecuteAsync(CreateVehicleCommand c,CancellationToken ct)
        {
            var dto = c.Request;

            var exists = await _context.Vehicles
                .AnyAsync(v => v.Plate == dto.Plate, ct);

            if (exists)
            {
                throw new ValidationException("Ya existe un vehículo con esa placa");
            }

            var existingInventory = _context.VehicleInventories
                .FirstOrDefault(v => v.Brand == dto.Brand && v.Model == dto.Model && v.Type == dto.Type);

            if (existingInventory != null)
            {
                existingInventory.AvailableQuantity += 1;
            }
            else
            {
                var newInventory = new VehicleInventory
                {
                    Id = Guid.NewGuid(),
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Type = dto.Type,
                    AvailableQuantity = 1
                };
                _context.VehicleInventories.Add(newInventory);
                existingInventory = newInventory;
            }

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Price = dto.Price,
                Plate = dto.Plate,
                Brand = dto.Brand,
                Model = dto.Model,
                Type = dto.Type,
                VehicleInventoryId = existingInventory.Id,
                IsSold = false
            };
            
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync(ct);

            return new CreateVehicleResponseDto
            {
                Id = vehicle.Id,
                Message = "El vehículo fue creado con éxito."
            };
        }
    }
}