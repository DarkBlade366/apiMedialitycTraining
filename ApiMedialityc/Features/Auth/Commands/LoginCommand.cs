using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiMedialityc.Features.Auth.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Auth.Commands
{
    public class LoginCommand
        : ICommand<LoginResponseDto>
    {
        public LoginRequestDto Request { get; set; }

        public LoginCommand (LoginRequestDto request)
        {
            Request = request;
        }
    }
}