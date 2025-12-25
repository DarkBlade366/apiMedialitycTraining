using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class DeleteUserEmailHandler
        : CommandHandler<DeleteUserEmailCommand, DeleteUserEmailResponseDto>
    {
        private readonly ApiDbContext _context;

        public DeleteUserEmailHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<DeleteUserEmailResponseDto> ExecuteAsync(DeleteUserEmailCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users
                .Include(u => u.Emails) 
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var email = user.Emails.FirstOrDefault(e => e.Email == dto.Email);

            if (email == null)
            {
                throw new Exception("Correo no encontrado"); 
            }

            if (user.Emails.Count <= 1)
            {
                throw new Exception("No se puede eliminar el último correo del usuario"); 
            }

            _context.UserEmails.Remove(email);
            await _context.SaveChangesAsync(ct);

            return new DeleteUserEmailResponseDto 
            { 
                Id = user.Id, 
                Email = dto.Email, 
                Message = "Correo eliminado correctamente" 
            };
        }
    }
}