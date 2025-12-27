using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.Enum;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class GetVehiclesHandler
        : ICommandHandler<GetVehiclesQuery, PagedResponse<GetVehiclesResponseDto>>
    {
        private readonly ApiDbContext _context;

        public GetVehiclesHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<GetVehiclesResponseDto>> ExecuteAsync(GetVehiclesQuery query, CancellationToken ct)
        {
            var c = query.Request;
            
            var queryDb = _context.Vehicles
                .Include(v => v.VehicleInventory)
                .Include(v => v.Sales)
                .AsQueryable();

            // Filtros de búsqueda
            if (c.Type.HasValue)
            {
                queryDb = queryDb.Where(v => v.Type == c.Type.Value);
            }

            if (!string.IsNullOrEmpty(c.Brand))
            {
                queryDb = queryDb.Where(v => v.Brand.Contains(c.Brand));
            }

            if (!string.IsNullOrEmpty(c.Model))
            {
                queryDb = queryDb.Where(v => v.Model.Contains(c.Model));
            }              

            if (c.Availability.HasValue && c.Availability.Value)
            {
                queryDb = queryDb.Where(v =>
                    v.VehicleInventory.AvailableQuantity > 0 &&
                    !v.IsSold &&
                    !v.Sales.Any(s => s.Status == SaleStatus.Pending)
                );
            }

            if (c.MinPrice.HasValue)
            {
                queryDb = queryDb.Where(v => v.Price >= c.MinPrice.Value);
            }
            
            if (c.MaxPrice.HasValue)
            {
                queryDb = queryDb.Where(v => v.Price <= c.MaxPrice.Value);
            }


            // Filtrado por rol (no admin ve solo vehículos no vendidos ni reservados)
            if (!query.IsAdmin)
            {
                queryDb = queryDb.Where(v =>
                    !v.IsSold &&
                    !v.Sales.Any(s => s.Status == SaleStatus.Pending)
                );
            }
            // Paginación
            var totalItems = await queryDb.CountAsync(ct);
            var totalPages = (int)Math.Ceiling((double)totalItems / (double)c.PageSize);

            var vehicles = await queryDb
                .OrderBy(v => v.Plate)
                .Skip((c.Page - 1) * c.PageSize)
                .Take(c.PageSize)
                .Select(v => new GetVehiclesResponseDto
                {
                    Id = v.Id,
                    Price = v.Price,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    Type = v.Type,
                    Availability = v.VehicleInventory.AvailableQuantity > 0,
                    AvailableQuantity = v.VehicleInventory.AvailableQuantity,
                    IsSold = v.IsSold
                })
                .ToListAsync(ct);

            return new PagedResponse<GetVehiclesResponseDto>
            {
                Items = vehicles,
                Page = c.Page,
                PageSize = c.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }
}
