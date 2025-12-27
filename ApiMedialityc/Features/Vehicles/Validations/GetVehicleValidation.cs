using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using ApiMedialityc.Features.Vehicles.Queries;
using FluentValidation;

namespace ApiMedialityc.Features.Vehicles.Validations
{
    public class GetVehicleValidation 
        : AbstractValidator<GetVehicleRequestDto>
    {
        public GetVehicleValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El ID del vehículo es obligatorio");
        }
    }
}