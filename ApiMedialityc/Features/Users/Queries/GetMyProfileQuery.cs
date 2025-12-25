using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Queries
{
    public class GetMyProfileQuery 
        : ICommand<GetMyProfileResponseDto>
    {
        public Guid UserId { get; set; }

        public GetMyProfileQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
