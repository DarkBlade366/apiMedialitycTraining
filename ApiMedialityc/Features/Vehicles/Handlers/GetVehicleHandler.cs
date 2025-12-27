using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.Enum;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class GetVehicleHandler 
        : CommandHandler<GetVehicleQuery, GetVehicleResponseDto>
    {
        private readonly ApiDbContext _context;

        public GetVehicleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<GetVehicleResponseDto> ExecuteAsync(GetVehicleQuery q, CancellationToken ct)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleInventory)
                .Include(v => v.Sales)
                .FirstOrDefaultAsync(v => v.Id == q.Request.Id, ct);

            if (vehicle == null)
            {
                throw new ValidationException("Vehículo no encontrado");
            }

            var inventoryStatus = "Avaliable";

            if (vehicle.IsSold)
            {
                inventoryStatus = "Sold";
            }
            if (vehicle.Sales.Any(s => s.Status == SaleStatus.Pending))
            {
                inventoryStatus = "Pending";
            }

            return new GetVehicleResponseDto
            {
                Id = vehicle.Id,
                Price = vehicle.Price,
                Plate = vehicle.Plate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Type = vehicle.Type,
                VehicleInventoryId = vehicle.VehicleInventoryId,
                AvailableQuantity = vehicle.VehicleInventory.AvailableQuantity,
                IsSold = vehicle.IsSold,
                InventoryStatus = inventoryStatus
            };
        }
    }
}