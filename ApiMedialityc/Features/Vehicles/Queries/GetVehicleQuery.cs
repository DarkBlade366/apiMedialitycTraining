using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Queries
{
    public class GetVehicleQuery
        : ICommand<GetVehicleResponseDto>
    {
        public GetVehicleRequestDto Request { get; set; }

        public GetVehicleQuery(GetVehicleRequestDto request)
        {
            Request = request;
        }
    }
}