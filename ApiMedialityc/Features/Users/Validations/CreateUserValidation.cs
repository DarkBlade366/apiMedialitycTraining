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
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Es obligatorio su nombre")
                .MaximumLength(100);
            RuleFor(x => x.Emails) 
                .NotEmpty().WithMessage("Debe tener al menos un correo");   
            RuleForEach(x => x.Emails)  
                .ChildRules(email =>    
                {   
                    email.RuleFor(e => e.Email)     
                        .NotEmpty().WithMessage("El correo no puede estar vacío") 
                        .EmailAddress().WithMessage("El correo debe ser válido"); 
                });
            RuleFor(x => x.Phones)
                .NotEmpty().WithMessage("Debe tener al menos un telefono");
        }
    }
}