using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Queries
{
    public class GetUsersQuery
        : ICommand<PagedResponse<GetUsersResponseDto>>
    {
        public GetUsersRequestDto Request { get; set; }

        public GetUsersQuery (GetUsersRequestDto request)
        {
            Request = request;
        }
    }
}