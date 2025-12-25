using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Client
{
    public class UpdateNameUserEndpoint
        : Endpoint<UpdateNameUserRequestDto, UpdateNameUserResponseDto>
    {
        public override void Configure()
        {
            Put("/users/{id}/name");
            Roles("Admin", "User");
            Validator<UpdateNameUserValidation>();
            Summary(s =>
            {
                s.Summary = "Actualiza solo el nombre de un usuario";
                s.Description = "Permite modificar únicamente el campo FullName de un usuario existente";
                s.ExampleRequest = new UpdateNameUserRequestDto 
                { 
                    FullName = "Name",
                };
            });
        }

        public override async Task HandleAsync(UpdateNameUserRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new UpdateNameUserCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}