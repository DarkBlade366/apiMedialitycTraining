using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Vehicles.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Vehicles.Validations
{
    public class GetVehiclesValidation
        : AbstractValidator<GetVehiclesRequestDto>
    {
        public GetVehiclesValidation()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("La página debe ser mayor que 0.");
            
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de la página debe ser mayor que 0.");

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MaxPrice > x.MinPrice)
                .WithMessage("El precio máximo debe ser mayor que el precio mínimo.");
        }
    }
}