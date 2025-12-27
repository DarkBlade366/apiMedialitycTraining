using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Handlers;
using ApiMedialityc.Features.Vehicles.Queries;
using ApiMedialityc.Features.Vehicles.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Endpoints.Admin
{
    public class GetVehicleEndpoint
        : Endpoint<GetVehicleRequestDto, GetVehicleResponseDto>
    {
        public override void Configure()
        {
            Get("/vehicles/{id}");
            Roles("Admin");
            Validator<GetVehicleValidation>();
            Summary(s =>
            {
                s.Summary = "Obtener vehículo por ID";
                s.Description = "Obtiene los detalles completos de un vehículo específico, incluyendo la información de su inventario. Solo para administradores.";
                s.ExampleRequest = new GetVehicleRequestDto{};
            });
        }

        public override async Task HandleAsync(GetVehicleRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var query = new GetVehicleQuery(req);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}