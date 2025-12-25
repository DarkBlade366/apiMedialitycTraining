using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class DeleteUserEmailValidation
        : AbstractValidator<DeleteUserEmailRequestDto>
    {
        public DeleteUserEmailValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("El id es obligatorio");
            RuleFor(x => x.Email) 
                .NotEmpty().WithMessage("Debe tener un correo") 
                .EmailAddress().WithMessage("El correo debe ser válido");
        }
    }
}