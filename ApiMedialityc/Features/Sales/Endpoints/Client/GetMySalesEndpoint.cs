using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Queries;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Endpoints.Client
{
    public class GetMySalesEndpoint
        : Endpoint<GetMySalesRequestDto, PagedResponse<GetMySalesResponseDto>>
    {
        public override void Configure()
        {
            Get("/sales/me");
            Roles("Admin", "User");
            Validator<GetMySalesValidation>();
            Summary(s =>
            {
                s.Summary = "Obtener mis ventas";
                s.Description = "Lista las ventas del usuario autenticado con filtros y paginación.";
                s.ExampleRequest = new GetMySalesRequestDto
                {
                    Page = 1,
                    PageSize = 10
                };
            });
        }

        public override async Task HandleAsync(GetMySalesRequestDto req, CancellationToken ct)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var query = new GetMySalesQuery(userId, req);
            var result = await query.ExecuteAsync(ct);
            await Send.OkAsync(result, ct);
        }
    }
}
