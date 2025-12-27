using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ApiMedialityc.Features.Sales.DTOs;

namespace ApiMedialityc.Features.Sales.Validations
{
    public class SalesReportValidation 
        : AbstractValidator<SalesReportRequestDto>
    {
        public SalesReportValidation()
        {
            RuleFor(x => x.Status)
                .Must(s => string.IsNullOrEmpty(s) || s == "Pending" || s == "Completed" || s == "Cancelled")
                .WithMessage("Status debe ser Pending, Completed o Cancelled si se especifica.");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From)
                .When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("El campo 'To' debe ser mayor o igual a 'From'.");
        }
    }
}
