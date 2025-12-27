using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Commands
{
    public class UpdateVehicleCommand
        : ICommand<UpdateVehicleResponseDto>
    {
        public UpdateVehicleRequestDto Request { get; set; }

        public UpdateVehicleCommand(UpdateVehicleRequestDto request)
        {
            Request = request;
        }
    }
}