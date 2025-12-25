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
    public class AddUserEmailEndpoint
        : Endpoint<AddUserEmailRequestDto, AddUserEmailResponseDto>
    {
        public override void Configure()
        {
            Post("/users/{id}/emails"); 
            Roles("Admin", "User");
            Validator<AddUserEmailValidation>();
            Summary(s => 
            { 
                    s.Summary = "Añade un nuevo email a un usuario"; 
                    s.Description = "Permite agregar un correo electrónico vinculado a un usuario existente"; 
                    s.ExampleRequest = new AddUserEmailRequestDto
                    {
                        Email = "correo@gmail.com"
                    };
            });
        }

        public override async Task HandleAsync(AddUserEmailRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new AddUserEmailCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}