using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class UpdateNameUserHandler
        : CommandHandler<UpdateNameUserCommand, UpdateNameUserResponseDto>
    {
        public readonly ApiDbContext _context;

        public UpdateNameUserHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<UpdateNameUserResponseDto> ExecuteAsync(UpdateNameUserCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            // Buscar usuario
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            
            user.FullName = dto.FullName;

            await _context.SaveChangesAsync();

            return new UpdateNameUserResponseDto
            {
                Id = user.Id,
                Message = "Nombre actualizado correctamente"
            };
        }
    }
}