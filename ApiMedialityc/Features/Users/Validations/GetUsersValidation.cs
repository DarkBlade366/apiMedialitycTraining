using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class GetUsersValidation
        : AbstractValidator<GetUsersRequestDto>
    {
        public GetUsersValidation()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("La página debe ser mayor que 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0");

            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("El nombre completo no puede exceder los 100 caracteres");
        }
    }
}