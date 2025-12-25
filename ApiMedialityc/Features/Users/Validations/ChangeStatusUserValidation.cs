using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class ChangeStatusUserValidation
        : AbstractValidator<ChangeStatusUserRequestDto>
    {
        public ChangeStatusUserValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id es obligatorio");
            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("Debe indicar el estado");
        }
    }
}