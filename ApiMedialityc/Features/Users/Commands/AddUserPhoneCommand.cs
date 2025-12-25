using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class AddUserPhoneCommand
        : ICommand<AddUserPhoneResponseDto>
    {
        public AddUserPhoneRequestDto Request { get; set; }

        public AddUserPhoneCommand(AddUserPhoneRequestDto request)
        {
            Request = request;
        }
    }
}