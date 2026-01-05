using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ApiMedialityc.Features.Users.Enum;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class AssignUserRoleHandler
        : CommandHandler<AssignUserRoleCommand, AssignUserRoleResponseDto>
    {
        private readonly ApiDbContext _context;

        public AssignUserRoleHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<AssignUserRoleResponseDto> ExecuteAsync(AssignUserRoleCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, ct);

            if (user == null)
            {
                throw new ValidationException("Usuario no encontrado"); 
            }

            if (!System.Enum.TryParse<Role>(dto.Role, true, out var newRole))
            {
                throw new ValidationException("Rol inválido");
            }

            if (user.Role == newRole)
            {
                throw new ValidationException($"El usuario ya tiene el rol '{newRole}'.");
            }

            user.Role = newRole;

            await _context.SaveChangesAsync(ct);

            return new AssignUserRoleResponseDto
            {
                UserId = user.Id,
                Role = newRole.ToString(),
                Message = "Rol asignado correctamente"
            };
        }
    }
}
