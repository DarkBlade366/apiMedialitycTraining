using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Commands
{
    public class AssignUserRoleCommand
        : ICommand<AssignUserRoleResponseDto>
    {
        public AssignUserRoleRequestDto Request { get; set; }

        public AssignUserRoleCommand(AssignUserRoleRequestDto request)
        {
            Request = request;
        }
    }
}
