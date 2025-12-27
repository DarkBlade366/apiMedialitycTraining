using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Handlers;
using ApiMedialityc.Features.Users.Queries;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Admin
{
    public class GetUsersEndpoint 
        : Endpoint<GetUsersRequestDto, PagedResponse<GetUsersResponseDto>>
    {
        public override void Configure()
        {
            Get ("/users/list");
            Roles("Admin");
            Validator<GetUsersValidation>(); 
            Summary(s =>
            {
                s.Summary = "Listo los datos de los usuarios por filtros";
                s.Description = "Puede listar usuarios por nombre, activos o no, usando paginacion";
                s.ExampleRequest = new GetUsersRequestDto
                {
                    Page = 1,
                    PageSize = 10
                }; 
            });
        }

        public override async Task HandleAsync(GetUsersRequestDto req, CancellationToken ct)
        {
            var query = new GetUsersQuery(req);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}