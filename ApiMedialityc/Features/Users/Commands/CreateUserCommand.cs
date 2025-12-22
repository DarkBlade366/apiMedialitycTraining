using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;

namespace ApiMedialityc.Features.Users.Commands
{
    public class CreateUserCommand
    {
        public CreateUserRequestDto _request { get; set; }

        public CreateUserCommand (CreateUserRequestDto request)
        {
            _request = request;
        }
    }
}