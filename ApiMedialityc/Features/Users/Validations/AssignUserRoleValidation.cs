using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc.Features.Users.Validations
{
    public class AssignUserRoleValidation
        : AbstractValidator<AssignUserRoleRequestDto>
    {
        public AssignUserRoleValidation()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("El UserId es obligatorio");

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(r => r == "Admin" || r == "User")
                .WithMessage("El rol debe ser Admin o User");
        }
    }
}
