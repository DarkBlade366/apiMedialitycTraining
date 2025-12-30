using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class UpdateUserCommand
        : ICommand<UpdateUserResponseDto>
    {
        public UpdateUserRequestDto Request { get; set; }
        public UpdateUserCommand(UpdateUserRequestDto request)
        {
            Request = request;
        }
    }
}