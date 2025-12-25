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
    public class DeleteUserEmailEndpoint
        : Endpoint<DeleteUserEmailRequestDto, DeleteUserEmailResponseDto>
    {
        public override void Configure() 
        { 
            Delete("/users/{id}/emails/{email}"); 
            Roles("Admin", "User");
            Validator<DeleteUserEmailValidation>();
            Summary(s => 
            { 
                s.Summary = "Elimina un email de un usuario"; 
                s.Description = "Permite eliminar un correo electrónico, excepto si es el último que queda"; 
                s.ExampleRequest = new DeleteUserEmailRequestDto
                {
                    Email = "correo@gmail.com"
                };
            }); 
        }

        public override async Task HandleAsync(DeleteUserEmailRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id"); 
            req.Email = Route<string>("email")!; 

            var command = new DeleteUserEmailCommand(req); 
            var response = await command.ExecuteAsync(ct); 
            await Send.OkAsync(response, ct);
        }
    }
}