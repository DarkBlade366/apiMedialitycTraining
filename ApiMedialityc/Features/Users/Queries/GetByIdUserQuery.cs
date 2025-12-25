using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Queries
{
    public class GetByIdUserQuery
        : ICommand<GetByIdUserResponseDto>
    {
        public GetByIdUserRequestDto Request { get; set; }

        public GetByIdUserQuery (GetByIdUserRequestDto request)
        {
            Request = request;
        }
    }
}