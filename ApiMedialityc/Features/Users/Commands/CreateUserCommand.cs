using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class CreateUserCommand
        : ICommand<CreateUserResponseDto>
    {
        public CreateUserRequestDto Request { get; set; }

        public CreateUserCommand (CreateUserRequestDto request)
        {
            Request = request;
        }
    }
}