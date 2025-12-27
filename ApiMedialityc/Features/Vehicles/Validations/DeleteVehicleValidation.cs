using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Vehicles.Validations
{
    public class DeleteVehicleValidation
        : AbstractValidator<DeleteVehicleRequestDto>
    {
        public DeleteVehicleValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id del vehículo es obligatorio");
        }
    }
}
