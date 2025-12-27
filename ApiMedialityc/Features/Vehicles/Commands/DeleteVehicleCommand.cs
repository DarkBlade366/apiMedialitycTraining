using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiMedialityc.Features.Vehicles.DTOs;
using FastEndpoints;

namespace ApiMedialityc.Features.Vehicles.Commands
{
    public class DeleteVehicleCommand
        : ICommand<DeleteVehicleResponseDto>
    {
        public DeleteVehicleRequestDto Request { get; set; }

        public DeleteVehicleCommand(DeleteVehicleRequestDto request)
        {
            Request = request;
        }
    }
} 