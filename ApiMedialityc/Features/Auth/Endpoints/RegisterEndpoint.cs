using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Auth.Endpoints
{
    public class RegisterEndpoint 
        : Endpoint<CreateUserRequestDto, CreateUserResponseDto>
    {
        public override void Configure()
        {
            Post("/auth/register");
            AllowAnonymous();
            Validator<CreateUserValidation>();
            Summary(s =>
            {
                s.Summary = "Registro de usuario";
                s.Description = "Permite crear un nuevo usuario con rol User";
                s.ExampleRequest = new CreateUserRequestDto
                {
                    FullName = "Name FirstLastName SecondLastName",
                    Password = "xxxxxxxxx",
                    Emails = new List<UserEmailDto> 
                    { 
                        new UserEmailDto 
                        { 
                            Email = "correo@gmail.com" 
                        } 
                    },
                    Phones = new List<UserPhoneDto> 
                    { 
                        new UserPhoneDto 
                        { 
                            Phone = "+XX XXXXXXX" 
                        } 
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
