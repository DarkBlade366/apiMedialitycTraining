using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ApiMedialityc.Features.Sales.DTOs;

namespace ApiMedialityc.Features.Sales.Validations
{
    public class CreateSaleValidation : AbstractValidator<CreateSaleRequestDto>
    {
        public CreateSaleValidation()
        {
            RuleFor(x => x.VehicleId)
                .NotEqual(Guid.Empty)
                .WithMessage("El id del vehículo es obligatorio.");
        }
    }
}
