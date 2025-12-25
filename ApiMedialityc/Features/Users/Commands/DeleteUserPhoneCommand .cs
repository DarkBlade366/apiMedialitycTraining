using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class DeleteUserPhoneCommand 
        : ICommand<DeleteUserPhoneResponseDto>
    {
        public DeleteUserPhoneRequestDto Request { get; set; }

        public DeleteUserPhoneCommand(DeleteUserPhoneRequestDto request)
        {
            Request = request;
        }
    }
}