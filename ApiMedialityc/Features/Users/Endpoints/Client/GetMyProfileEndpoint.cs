using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Queries;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Client
{
    public class GetMyProfileEndpoint 
        : EndpointWithoutRequest<GetMyProfileResponseDto>
    {
        public override void Configure()
        {
            Get("/users/me");
            Roles("Admin", "User");
            Summary(s =>
            {
                s.Summary = "Obtiene el perfil del usuario autenticado";
                s.Description = "Devuelve la información del usuario actual usando el UserId del token";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == "sub").Value);

            var query = new GetMyProfileQuery(userId);
            var response = await query.ExecuteAsync(ct);

            await Send.OkAsync(response, ct);
        }
    }
}
