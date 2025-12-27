using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Vehicles.Commands;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Handlers;
using ApiMedialityc.Features.Vehicles.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Endpoints.Admin
{
    public class DeleteVehicleEndpoint
        : Endpoint<DeleteVehicleRequestDto, DeleteVehicleResponseDto>
    {
        public override void Configure()
        {
            Delete("/vehicles/delete/{id}");
            Roles("Admin");
            Validator<DeleteVehicleValidation>();
            Summary(s =>
            {
                s.Summary = "Eliminar vehículo";
                s.Description = "Elimina un vehículo específico y actualiza su inventario. Solo para administradores.";
                s.ExampleRequest = new GetVehicleRequestDto{};
            });
        }

        public override async Task HandleAsync(DeleteVehicleRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new DeleteVehicleCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
