using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Admin
{
    public class AssignUserRoleEndpoint
        : Endpoint<AssignUserRoleRequestDto, AssignUserRoleResponseDto>
    {
        public override void Configure()
        {
            Put("/users/assign-role");
            Roles("Admin");
            Validator<AssignUserRoleValidation>();
            Summary(s =>
            {
                s.Summary = "Asignar rol a un usuario";
                s.Description = "Permite a un administrador asignar o cambiar el rol de un usuario.";
                s.ExampleRequest = new AssignUserRoleRequestDto
                {
                    Role = "User"
                };
            });
        }

        public override async Task HandleAsync(AssignUserRoleRequestDto req, CancellationToken ct)
        {
            var command = new AssignUserRoleCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
