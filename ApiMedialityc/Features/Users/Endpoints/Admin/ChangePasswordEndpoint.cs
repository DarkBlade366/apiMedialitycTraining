using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Validations;
using ApiMedialityc.Features.Users.Handlers;
using FastEndpoints;
using System.Security.Claims;

namespace ApiMedialityc.Features.Auth.Endpoints
{
    public class ChangePasswordEndpoint
        : Endpoint<ChangePasswordRequestDto, ChangePasswordResponseDto>
    {
        public override void Configure()
        {
            Put("/users/change-password");
            Roles("Admin", "User");
            Validator<ChangePasswordValidation>();
            Summary(s =>
            {
                s.Summary = "Cambiar contraseña del usuario autenticado";
                s.Description = "Permite al usuario cambiar su propia contraseña.";
                s.ExampleRequest = new ChangePasswordRequestDto
                {
                    OldPassword = "Admin123*",
                    NewPassword = "NuevaPass123",
                    ConfirmPassword = "NuevaPass123"
                };
            });
        }

        public override async Task HandleAsync(ChangePasswordRequestDto req, CancellationToken ct)
        {
            var Id = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            req.Id = Id;

            var command = new ChangePasswordCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}
