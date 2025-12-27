using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiMedialityc.Features.Sales.Handlers
{
    public class GetSaleByIdHandler
        : ICommandHandler<GetSaleByIdQuery, GetSaleByIdResponseDto>
    {
        private readonly ApiDbContext _context;

        public GetSaleByIdHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<GetSaleByIdResponseDto> ExecuteAsync(GetSaleByIdQuery query, CancellationToken ct)
        {
            var req = query.Request;

            var sale = await _context.Sales
                .AsNoTracking()
                .Include(s => s.Vehicle)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == req.Id, ct);

            if (sale == null)
            {
                throw new ValidationException("La venta no existe.");
            }

            if (!query.IsAdmin && sale.UserId != query.CurrentUserId)
            {
                throw new UnauthorizedAccessException("No tienes acceso a esta venta.");
            }

            return new GetSaleByIdResponseDto
            {
                SaleId = sale.Id,
                Status = sale.Status.ToString(),
                Price = sale.Price,
                CreatedAt = sale.SaleDate,

                UserId = sale.UserId,
                UserName = sale.User.FullName,

                VehicleId = sale.Vehicle.Id,
                VehicleBrand = sale.Vehicle.Brand,
                VehicleModel = sale.Vehicle.Model,
                VehicleType = sale.Vehicle.Type.ToString()
            };
        }
    }
}
