using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Vehicles.Validations
{
    public class UpdateVehicleValidation
        : AbstractValidator<UpdateVehicleRequestDto>
    {
        public UpdateVehicleValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id del vehículo es obligatorio");

            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage("La placa es obligatoria");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("La marca es obligatoria");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("El modelo es obligatorio");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Tipo de vehículo inválido");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a 0");
        }
    }
}
