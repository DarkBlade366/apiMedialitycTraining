using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class CreateUserValidation
        : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.fullName)
                .NotEmpty().WithMessage("Es obligatorio su nombre")
                .MaximumLength(100);
            RuleFor(x => x.emails)
                .NotEmpty().WithMessage("Debe tener al menos un correo")
                .Must(list => list.All(e => e.email.Contains("@")))
                .WithMessage("Todos los correos deben ser válidos");
            RuleFor(x => x.phones)
                .NotEmpty().WithMessage("Debe tener al menos un telefono");
        }
    }
}