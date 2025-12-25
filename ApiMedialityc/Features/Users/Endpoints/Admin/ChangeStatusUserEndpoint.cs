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
    public class ChangeStatusUserEndpoint
        : Endpoint<ChangeStatusUserRequestDto, ChangeStatusUserResponseDto>
    {
        public override void Configure()
        {
            Put ("/users/status/{id}");
            Roles("Admin");
            Validator<ChangeStatusUserValidation>();
            Summary(s =>
            {
                s.Summary = "Actualiza el estado de un usuario";
                s.Description = "Actualiza el estado de un usuario, puede activar o desactivar al usuario por su id";
                s.ExampleRequest = new ChangeStatusUserRequestDto
                {
                    IsActive = true
                };
            });
        }

        public override async Task HandleAsync(ChangeStatusUserRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new ChangeStatusUserCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}