using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class UpdateNameUserValidation
        : AbstractValidator<UpdateNameUserRequestDto>
    {
        public UpdateNameUserValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id es obligatorio");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Es obligatorio su nombre")
                .MaximumLength(100);
        }
    }
}