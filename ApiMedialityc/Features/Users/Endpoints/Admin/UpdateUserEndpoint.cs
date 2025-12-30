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
    public class UpdateUserEndpoint
        : Endpoint<UpdateUserRequestDto, UpdateUserResponseDto>
    {
        public override void Configure()
        {
            Put("/users/{id}");
            Roles("Admin");
            Validator<UpdateUserValidation>();
            Summary(s =>
            {
                s.Summary = "Actualiza un usuario";
                s.Description = "Actualiza un usuario existente";
                s.ExampleRequest = new UpdateUserRequestDto 
                { 
                    FullName = "Name FirstLastName SecondLastName",
                    Emails = new List<UserEmailDto> 
                    { 
                        new UserEmailDto { Email = "correo@gmail.com" } 
                    }, 
                    Phones = new List<UserPhoneDto> 
                    { 
                        new UserPhoneDto { Phone = "+XX XXXXXXX" } 
                    } 
                };
            });
        }

        public override async Task HandleAsync(UpdateUserRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id");
            
            var command = new UpdateUserCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}