using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class DeleteUserEmailCommand
        : ICommand<DeleteUserEmailResponseDto>
    {
        public DeleteUserEmailRequestDto Request { get; set; }

        public DeleteUserEmailCommand(DeleteUserEmailRequestDto request)
        {
            Request = request;
        }
    }
}