using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Common.Security;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class ChangePasswordHandler
        : CommandHandler<ChangePasswordCommand, ChangePasswordResponseDto>
    {
        private readonly ApiDbContext _context;

        public ChangePasswordHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<ChangePasswordResponseDto> ExecuteAsync(ChangePasswordCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            // Obtener usuario por FullName
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new ValidationException("Usuario no encontrado");
            }

            // Verificar contraseña actual
            if (!PasswordHasher.Verify(dto.OldPassword, user.Password))
            {
                throw new ValidationException("Contraseña actual incorrecta");
            }

            // Cambiar contraseña
            user.Password = PasswordHasher.Hash(dto.NewPassword);
            user.MustChangePassword = false;

            await _context.SaveChangesAsync(ct);

            return new ChangePasswordResponseDto
            {
                Message = "Contraseña cambiada correctamente"
            };
        }
    }
}
