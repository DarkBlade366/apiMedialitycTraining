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
    public class UpdateVehicleEndpoint
        : Endpoint<UpdateVehicleRequestDto, UpdateVehicleResponseDto>
    {
        public override void Configure()
        {
            Put("/vehicles/update/{id}");
            Roles("Admin");
            Validator<UpdateVehicleValidation>();
            Summary(s =>
            {
                s.Summary = "Actualizar vehículo";
                s.Description = "Actualiza la información de un vehículo existente. Solo para administradores.";
                s.ExampleRequest = new UpdateVehicleRequestDto
                {
                    Plate = "ABC-123",
                    Brand = "Toyota",
                    Model = "Corolla",
                    Type = 0,
                };
            });
        }

        public override async Task HandleAsync(UpdateVehicleRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new UpdateVehicleCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
