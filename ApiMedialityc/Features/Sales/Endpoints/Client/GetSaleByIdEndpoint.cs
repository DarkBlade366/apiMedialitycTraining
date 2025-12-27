using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using ApiMedialityc.Features.Sales.Queries;
using ApiMedialityc.Features.Sales.Validations;
using FastEndpoints;
using System.Security.Claims;

namespace ApiMedialityc.Features.Sales.Endpoints.Client
{
    public class GetSaleByIdEndpoint
        : Endpoint<GetSaleByIdRequestDto, GetSaleByIdResponseDto>
    {
        public override void Configure()
        {
            Get("/sales/{id}");
            Roles("Admin", "User");
            Validator<GetSaleByIdValidation>();
            Summary(s =>
            {
                s.Summary = "Obtener venta por ID";
                s.Description = "Admin puede ver cualquier venta. El usuario solo puede ver sus propias ventas.";
                s.ExampleRequest = new GetSaleByIdRequestDto{};
            });
        }

        public override async Task HandleAsync(GetSaleByIdRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");
            var currentUserId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var isAdmin = User.IsInRole("Admin");

            var query = new GetSaleByIdQuery(req, currentUserId, isAdmin);
            var result = await query.ExecuteAsync(ct);
            await Send.OkAsync(result, ct);
        }
    }
}
