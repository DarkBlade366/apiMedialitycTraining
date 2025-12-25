using ApiMedialityc.Features.Auth.Commands;
using ApiMedialityc.Features.Auth.DTOs;
using ApiMedialityc.Features.Auth.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Auth.Endpoints
{
    public class LoginEndpoint : Endpoint<LoginRequestDto, LoginResponseDto>
    {
        public override void Configure()
        {
            Post("/auth/login");
            AllowAnonymous();
            Validator<LoginValidation>();
            Summary(s =>
            {
                s.Summary = "Login de usuario";
                s.Description = "Genera un JWT válido si las credenciales son correctas";
                s.ExampleRequest = new LoginRequestDto
                {
                    FullName = "Name FirsLastName SecondLastName",
                    Password = "xxxxxxxxx"
                };
            });
        }

        public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
        {   
            var command = new LoginCommand(req);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}

