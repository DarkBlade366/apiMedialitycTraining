using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class ChangeStatusUserCommand
        : ICommand<ChangeStatusUserResponseDto>
    {
        public ChangeStatusUserRequestDto Request { get; set; }

        public ChangeStatusUserCommand (ChangeStatusUserRequestDto request)
        {
            Request = request;
        }
    }
}