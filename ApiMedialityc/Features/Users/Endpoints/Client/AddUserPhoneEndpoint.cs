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
    public class AddUserPhoneEndpoint
    : Endpoint<AddUserPhoneRequestDto, AddUserPhoneResponseDto>
    {
        public override void Configure()
        {
            Post("/users/{id}/phones"); 
            Roles("Admin", "User"); 
            Validator<AddUserPhoneValidation>();
            Summary(s => 
            { 
                    s.Summary = "Añade un nuevo phone a un usuario"; 
                    s.Description = "Permite agregar un telefono vinculado a un usuario existente"; 
                    s.ExampleRequest = new AddUserPhoneRequestDto
                    {
                        Phone = "+XX XXXXXXX"
                    };
            });
        }

        public override async Task HandleAsync(AddUserPhoneRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");

            var command = new AddUserPhoneCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}