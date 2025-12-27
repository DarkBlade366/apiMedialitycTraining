using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Endpoints.Admin
{
    public class GetSalesEndpoint 
        : Endpoint<GetSalesRequestDto, PagedResponse<GetSalesResponseDto>>
    {
        public override void Configure()
        {
            Get("/sales");
            Roles("Admin");
            Validator<GetSalesValidation>();
            Summary(s =>
            {
                s.Summary = "Obtener todas las ventas";
                s.Description = "Permite a un administrador ver todas las ventas con filtros y paginación.";
                s.ExampleRequest = new GetSalesRequestDto
                {
                    Page = 1,
                    PageSize = 10
                };
            });
        }

        public override async Task HandleAsync(GetSalesRequestDto req, CancellationToken ct)
        {
            var query = new GetSalesQuery(req);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}