using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Handlers;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;
using ApiMedialityc.Features.Sales.Queries;

namespace ApiMedialityc.Features.Sales.Endpoints.Admin
{
    public class SalesReportEndpoint 
        : Endpoint<SalesReportRequestDto, SalesReportResponseDto>
    {
        public override void Configure()
        {
            Get("/reports/sales");
            Roles("Admin");
            Validator<SalesReportValidation>();
            Summary(s =>
            {
                s.Summary = "Obtiene un reporte de ventas";
                s.Description = "Genera estadísticas de ventas filtradas por fechas, estado, vehículo o usuario.";
            });
        }

        public override async Task HandleAsync(SalesReportRequestDto req, CancellationToken ct)
        {
            var query = new SalesReportQuery(req);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
