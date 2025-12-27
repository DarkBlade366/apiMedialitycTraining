using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Queries;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Endpoints.Admin
{
    public class InventoryReportEndpoint
        : EndpointWithoutRequest<InventoryReportResponseDto>
    {
        public override void Configure()
        {
            Get("/reports/inventory");
            Roles("Admin");
            Summary(s =>
            {
                s.Summary = "Inventory report";
                s.Description = "Returns inventory status, availability and recent movements";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new InventoryReportQuery();
            var result = await query.ExecuteAsync(ct);
            await Send.OkAsync(result, ct);
        }
    }
}
