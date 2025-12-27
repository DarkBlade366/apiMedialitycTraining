using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Commands
{
    public class CreateVehicleCommand
        : ICommand<CreateVehicleResponseDto>
    {
        public CreateVehicleRequestDto Request { get; set; }

        public CreateVehicleCommand(CreateVehicleRequestDto request)
        {
            Request = request;
        }
    }
}