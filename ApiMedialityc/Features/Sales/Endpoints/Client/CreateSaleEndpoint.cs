using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Handlers;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Endpoints.Client
{
    public class CreateSaleEndpoint : Endpoint<CreateSaleRequestDto, CreateSaleResponseDto>
    {
        public override void Configure()
        {
            Post("/sales");
            Roles("Admin", "User");
            Validator<CreateSaleValidation>();
            Summary(s =>
            {
                s.Summary = "Registrar una venta pendiente";
                s.Description = "Crea una venta en estado Pending y actualiza el inventario del vehículo.";
                s.ExampleRequest = new CreateSaleRequestDto{};
            });
        }

        public override async Task HandleAsync(CreateSaleRequestDto req, CancellationToken ct)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var command = new CreateSaleCommand(req, userId);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
