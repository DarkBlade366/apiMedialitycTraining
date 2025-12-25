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
    public class DeleteUserPhoneEndpoint
        : Endpoint<DeleteUserPhoneRequestDto, DeleteUserPhoneResponseDto>
    {
        public override void Configure() 
        { 
            Delete("/users/{id}/phones/{phone}"); 
            Roles("Admin", "User");
            Validator<DeleteUserPhoneValidation>();
            Summary(s => 
            { 
                s.Summary = "Elimina un telefono de un usuario"; 
                s.Description = "Permite eliminar un telefono, excepto si es el último que queda";
                s.ExampleRequest = new DeleteUserPhoneRequestDto
                {
                    Phone = "+XX XXXXXXX"
                }; 
            }); 
        }

        public override async Task HandleAsync(DeleteUserPhoneRequestDto req, CancellationToken ct)
        {
            req.Id = Route<Guid>("id"); 
            req.Phone = Route<string>("phone")!; 

            var command = new DeleteUserPhoneCommand(req); 
            var response = await command.ExecuteAsync(ct); 
            await Send.OkAsync(response, ct);
        }
    }
}