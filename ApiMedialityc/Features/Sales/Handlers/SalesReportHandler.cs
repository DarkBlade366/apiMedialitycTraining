using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Enum;
using Microsoft.EntityFrameworkCore;
using ApiMedialityc.Features.Sales.Queries;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class SalesReportHandler 
        : ICommandHandler<SalesReportQuery, SalesReportResponseDto>
    {
        private readonly ApiDbContext _context;

        public SalesReportHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<SalesReportResponseDto> ExecuteAsync(SalesReportQuery command, CancellationToken ct)
        {
            var query = _context.Sales
                .Include(s => s.Vehicle)
                .Include(s => s.User)
                .AsQueryable();


            // Filtros
            if (command.Request.From.HasValue)
            {
                query = query.Where(s => s.SaleDate >= command.Request.From.Value);
            }

            if (command.Request.To.HasValue)
            {
                query = query.Where(s => s.SaleDate <= command.Request.To.Value);
            }

            if (!string.IsNullOrEmpty(command.Request.Status))
            {
                if (System.Enum.TryParse<SaleStatus>(command.Request.Status, true, out var status))
                {
                query = query.Where(s => s.Status == status);
                }
            }

            if (command.Request.VehicleId.HasValue)
            {
                query = query.Where(s => s.VehicleId == command.Request.VehicleId.Value);
            }

            if (command.Request.UserId.HasValue)
            {
                query = query.Where(s => s.UserId == command.Request.UserId.Value);
            }

            var salesList = await query.ToListAsync(ct);

            var completedSales = salesList
                .Where(s => s.Status == SaleStatus.Completed)
                .ToList();

            var response = new SalesReportResponseDto
            {
                TotalSales = completedSales.Count,
                TotalRevenue = completedSales.Sum(s => s.Price),

                SalesByStatus = salesList
                    .GroupBy(s => s.Status.ToString())
                    .ToDictionary(g => g.Key, g => g.Count()),

                SalesByVehicleType = completedSales
                    .GroupBy(s => s.Vehicle.Type.ToString())
                    .ToDictionary(g => g.Key, g => g.Count()),

                RecentSales = completedSales
                    .OrderByDescending(s => s.SaleDate)
                    .Take(10)
                    .Select(s => new SaleSummaryDto
                    {
                        SaleId = s.Id,
                        Status = s.Status.ToString(),
                        Price = s.Price,
                        SaleDate = s.SaleDate,
                        VehicleBrand = s.Vehicle.Brand,
                        VehicleModel = s.Vehicle.Model,
                        VehicleType = s.Vehicle.Type.ToString(),
                        UserName = s.User.FullName
                    })
                    .ToList()
            };

            return response;
        }
    }
}
