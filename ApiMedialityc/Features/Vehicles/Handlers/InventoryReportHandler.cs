using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.Enum;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Enum;
using ApiMedialityc.Features.Vehicles.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Vehicles.Handlers
{
    public class InventoryReportHandler
        : ICommandHandler<InventoryReportQuery, InventoryReportResponseDto>
    {
        private readonly ApiDbContext _context;

        public InventoryReportHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryReportResponseDto> ExecuteAsync(InventoryReportQuery command, CancellationToken ct)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Sales)
                .ToListAsync(ct);

            var response = new InventoryReportResponseDto
            {
                TotalVehicles = vehicles.Count,
                SoldVehicles = vehicles.Count(v => v.IsSold),

                AvailableVehicles = vehicles.Count(v =>
                    !v.IsSold &&
                    !v.Sales.Any(s => s.Status == SaleStatus.Pending)
                ),

                PendingVehicles = vehicles.Count(v =>
                    v.Sales.Any(s => s.Status == SaleStatus.Pending)
                ),

                VehiclesByType = vehicles
                    .GroupBy(v => v.Type.ToString())
                    .ToDictionary(g => g.Key, g => g.Count()),

                RecentMovements = vehicles
                    .SelectMany(v => v.Sales.Select(s => new
                    {
                        Vehicle = v,
                        Sale = s
                    }))
                    .OrderByDescending(x => x.Sale.SaleDate)
                    .Take(10)
                    .Select(x => new VehicleInventorySummaryDto
                    {
                        VehicleId = x.Vehicle.Id,
                        Brand = x.Vehicle.Brand,
                        Model = x.Vehicle.Model,
                        Type = x.Vehicle.Type.ToString(),
                        Status = x.Sale.Status.ToString(),
                        Date = x.Sale.SaleDate
                    })
                    .ToList()
            };

            return response;
        }
    }
}
