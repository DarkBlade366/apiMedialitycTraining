using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Commands;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Sales.Endpoints.Admin
{
    //Utilice EndpointWithoutRequest<TResponse> ya que no hay solicitud DTO pq se toma de la ruta
    // trate de usar en todos el Endpoint (nose si sea bueno) pero aqui creo q es casi obligatorio
    public class CompleteSaleEndpoint 
        : EndpointWithoutRequest<CompleteSaleResponseDto>
    {
        public override void Configure()
        {
            Put("/sales/{id}/complete");
            Roles("Admin");
            Summary(s =>
            {
                s.Summary = "Completar una venta";
                s.Description = "Cambia el estado de la venta de Pending a Completed y marca el vehículo como vendido.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var Id = Route<Guid>("id");

            //creo que se puede hacer solo con el Command, pero hice el Request para  respetar el patron, 
            // pero creo q igual lo respeto si no lo uso en esta ocacion
            var request = new CompleteSaleRequestDto { Id = Id };
            var command = new CompleteSaleCommand(request);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
