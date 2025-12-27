using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class GetMySalesHandler
        : ICommandHandler<GetMySalesQuery, PagedResponse<GetMySalesResponseDto>>
    {
        private readonly ApiDbContext _context;

        public GetMySalesHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<GetMySalesResponseDto>> ExecuteAsync(GetMySalesQuery query, CancellationToken ct)
        {
            var salesQuery = _context.Sales
                .AsNoTracking()
                .Include(s => s.Vehicle)
                .Where(s => s.UserId == query.UserId);

            if (query.Request.Status.HasValue)
            {
                salesQuery = salesQuery
                    .Where(s => s.Status == query.Request.Status.Value);
            }

            var totalItems = await salesQuery.CountAsync(ct);

            var items = await salesQuery
                .OrderByDescending(s => s.SaleDate)
                .Skip((query.Request.Page - 1) * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .Select(s => new GetMySalesResponseDto
                {
                    SaleId = s.Id,
                    Status = s.Status.ToString(),
                    SaleDate = s.SaleDate,
                    Price = s.Price,
                    Plate = s.Vehicle.Plate,
                    Brand = s.Vehicle.Brand,
                    Model = s.Vehicle.Model,
                    Type = s.Vehicle.Type.ToString()
                })
                .ToListAsync(ct);

            return new PagedResponse<GetMySalesResponseDto>
            {
                Items = items,
                Page = query.Request.Page,
                PageSize = query.Request.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / (double)query.Request.PageSize)
            };
        }
    }
}