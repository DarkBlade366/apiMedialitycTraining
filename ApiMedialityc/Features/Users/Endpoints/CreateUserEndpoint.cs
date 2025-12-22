using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Handlers;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints
{
    public class CreateUserEndpoint
        : Endpoint<CreateUserRequestDto, CreateUserResponseDto>
    {
        private readonly CreateUserHandler _handler;

        public CreateUserEndpoint(CreateUserHandler handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Post("/users");
            AllowAnonymous(); //por ahora despues sera Rol Admin
        }

        public override async Task HandleAsync(CreateUserRequestDto req, CancellationToken ct)
        {
            var command = new CreateUserCommand(req);
            var response = await _handler.HandlerAsync(command);
            await Send.OkAsync(response);
        }
    }
}