using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class AddUserEmailCommand
        : ICommand<AddUserEmailResponseDto>
    {
        public AddUserEmailRequestDto Request { get; set; }

        public AddUserEmailCommand(AddUserEmailRequestDto request)
        {
            Request = request;
        }
    }
}