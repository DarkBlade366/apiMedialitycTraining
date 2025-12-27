using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Sales.Validations
{
    public class GetMySalesValidation
        : AbstractValidator<GetMySalesRequestDto>
    {
        public GetMySalesValidation()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page debe ser mayor que 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(50)
                .WithMessage("PageSize debe estar entre 1 y 50.");
        }
    }
}
