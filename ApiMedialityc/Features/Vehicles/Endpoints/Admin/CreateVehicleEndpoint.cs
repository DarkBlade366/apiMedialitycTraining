using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.Commands;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Enum;
using ApiMedialityc.Features.Vehicles.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Endpoints
{
    public class CreateVehicleEndpoint
        : Endpoint<CreateVehicleRequestDto, CreateVehicleResponseDto>
    {
        public override void Configure()
        {
            Post("/vehicles");
            Roles("Admin");
            Validator<CreateVehicleValidation>();
            Summary(s =>
            {
                s.Summary = "Creación de vehículo";
                s.Description = "Crea un nuevo vehículo y su inventario automáticamente, solo para administradores.\n" + "Opciones para 'Type': \n0.Car, \n1.Van, \n2.Motorcycle, \n3.Truck, \n4.Other";
                s.ExampleRequest = new CreateVehicleRequestDto
                {
                    Plate = "ABC-123",
                    Brand = "Toyota",
                    Model = "Corolla",
                    Type = VehicleType.Car,
                };
            });
        }

        public override async Task HandleAsync(CreateVehicleRequestDto req, CancellationToken ct)
        {
            var command = new CreateVehicleCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}