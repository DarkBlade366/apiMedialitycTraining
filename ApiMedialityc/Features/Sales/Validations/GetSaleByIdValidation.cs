using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Sales.Validations
{
    public class GetSaleByIdValidation 
        : AbstractValidator<GetSaleByIdRequestDto>
    {
        public GetSaleByIdValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El id de la venta es obligatorio.");
        }
    }
}
