using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Vehicles.Validations
{
    public class CreateVehicleValidation
        : AbstractValidator<CreateVehicleRequestDto>
    {   
        public CreateVehicleValidation()
        {
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("La placa es obligatoria")
                .MaximumLength(20);

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("La marca es obligatoria");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("El modelo es obligatorio");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a 0");
        }
    }
}