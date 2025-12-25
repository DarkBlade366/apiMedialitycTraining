using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Handlers;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Admin
{
    public class CreateUserEndpoint
        : Endpoint<CreateUserRequestDto, CreateUserResponseDto>
    {
        public override void Configure()
        {
            Post("/admin/create");
            Roles("Admin");
            Validator<CreateUserValidation>();
            Summary(s =>
            {
                s.Summary = "Creacion de un usuario";
                s.Description = "Crea un nuevo usuario, por defecto es User";
                s.ExampleRequest = new CreateUserRequestDto 
                { 
                    FullName = "Name FirsLastName SecondLastName", 
                    Password = "xxxxxxxxx", 
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

        public override async Task HandleAsync(CreateUserRequestDto req, CancellationToken ct)
        {
            var command = new CreateUserCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}