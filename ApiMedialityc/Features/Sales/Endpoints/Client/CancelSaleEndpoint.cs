using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Handlers;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Endpoints.Client
{
    //Utilice EndpointWithoutRequest<TResponse> igual q en el complete ya que no hay solicitud DTO pq se toma de la ruta
    public class CancelSaleEndpoint
        : EndpointWithoutRequest<CancelSaleResponseDto>
    {
        public override void Configure()
        {
            Put("/sales/{id}/cancel");
            Roles("Admin", "User");
            Summary(s =>
            {
                s.Summary = "Cancelar una venta";
                s.Description = "Cambia el estado de Pending a Cancelled y devuelve el vehículo al inventario.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var Id = Route<Guid>("id");

            var userId = Guid.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var request = new CancelSaleRequestDto { Id = Id };
            var command = new CancelSaleCommand(request, userId);
            var response = await command.ExecuteAsync(ct);

            await Send.OkAsync(response, ct);
        }
    }
}