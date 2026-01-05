using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class ChangePasswordValidation
        : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordValidation()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("La contraseña actual es obligatoria");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(7)
                .WithMessage("La nueva contraseña debe tener al menos 7 caracteres");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("La confirmación de la contraseña no coincide");
        }
    }
}
