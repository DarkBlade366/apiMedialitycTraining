using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Queries
{
    public class GetVehiclesQuery
        : ICommand<PagedResponse<GetVehiclesResponseDto>>
    {
        public GetVehiclesRequestDto Request { get; set; }

        public bool IsAdmin { get; set; } = false;

        public GetVehiclesQuery(GetVehiclesRequestDto request, bool isAdmin)
        {
            Request = request;
            IsAdmin = isAdmin;
        }
    }
}