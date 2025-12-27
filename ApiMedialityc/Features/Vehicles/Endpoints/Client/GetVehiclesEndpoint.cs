using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.Queries;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Vehicles.Enum;
using ApiMedialityc.Features.Vehicles.Validations;
using System.Security.Claims;

namespace ApiMedialityc.Features.Vehicles.Endpoints
{
    public class GetVehiclesEndpoint
    : Endpoint<GetVehiclesRequestDto, PagedResponse<GetVehiclesResponseDto>>
    {
        public override void Configure()
        {
            Get("/vehicles");
            Roles("Admin", "User");
            Validator<GetVehiclesValidation>();
            Summary(s =>
            {
                s.Summary = "Obtener la lista de vehículos disponibles";
                s.Description = "Devuelve la lista de vehículos con filtros opcionales como tipo, marca, modelo y disponibilidad.";
                s.ExampleRequest = new GetVehiclesRequestDto
                {
                    Availability = true,
                    Page = 1,
                    PageSize = 10
                };
            });
        }

        public override async Task HandleAsync(GetVehiclesRequestDto req, CancellationToken ct)
        {
            var isAdmin = User.IsInRole("Admin");
            var query = new GetVehiclesQuery(req, isAdmin);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}