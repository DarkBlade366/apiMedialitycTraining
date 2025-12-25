using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class UpdateNameUserCommand
        : ICommand<UpdateNameUserResponseDto>
    {
        public UpdateNameUserRequestDto Request { get; set; }

        public UpdateNameUserCommand(UpdateNameUserRequestDto request)
        {
            Request = request;
        }
    }
}