using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class ChangePasswordCommand 
        : ICommand<ChangePasswordResponseDto>
    {
        public ChangePasswordRequestDto Request { get; set; }

        public ChangePasswordCommand(ChangePasswordRequestDto request)
        {
            Request = request;
        }
    }
}
