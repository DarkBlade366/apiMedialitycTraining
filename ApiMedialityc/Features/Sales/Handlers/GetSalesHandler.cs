using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class GetSalesHandler 
        : ICommandHandler<GetSalesQuery, PagedResponse<GetSalesResponseDto>>
    {
        private readonly ApiDbContext _context;

        public GetSalesHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<GetSalesResponseDto>> ExecuteAsync(GetSalesQuery query, CancellationToken ct)
        {
            var req = query.Request;

            var salesQuery = _context.Sales
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Vehicle)
                    .ThenInclude(v => v.VehicleInventory)
                .AsQueryable();

            if (req.Status.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.Status == req.Status.Value);
            }

            if (req.UserId.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.UserId == req.UserId.Value);
            }

            if (req.VehicleInventoryId.HasValue)
            {
                salesQuery = salesQuery.Where(s =>
                    s.Vehicle.VehicleInventoryId == req.VehicleInventoryId.Value);
            }

            var totalItems = await salesQuery.CountAsync(ct);

            var items = await salesQuery
                .OrderByDescending(s => s.SaleDate)
                .Skip((req.Page - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(s => new GetSalesResponseDto
                {
                    SaleId = s.Id,
                    Status = s.Status.ToString(),
                    Price = s.Price,
                    SaleDate = s.SaleDate,

                    UserId = s.UserId,
                    UserName = s.User.FullName,

                    VehicleId = s.VehicleId,
                    VehicleBrand = s.Vehicle.Brand,
                    VehicleModel = s.Vehicle.Model,
                    VehicleType = s.Vehicle.Type.ToString(),

                    VehicleInventoryId = s.Vehicle.VehicleInventoryId
                })
                .ToListAsync(ct);

            return new PagedResponse<GetSalesResponseDto>
            {
                Items = items,
                Page = req.Page,
                PageSize = req.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / (double)req.PageSize)
            };
        }
    }
}