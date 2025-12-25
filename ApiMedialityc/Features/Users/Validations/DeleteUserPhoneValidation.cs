using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class DeleteUserPhoneValidation
        : AbstractValidator<DeleteUserPhoneRequestDto>
    {
        public DeleteUserPhoneValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id es obligatorio");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Debe tener al menos un telefono");
        }
    }
}