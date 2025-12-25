using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Auth.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Auth.Validations
{
    public class LoginValidation
        : AbstractValidator<LoginRequestDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.FullName) 
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio"); 
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria");
        }
    }
}