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
    public class AddUserEmailHandler 
        : CommandHandler<AddUserEmailCommand, AddUserEmailResponseDto>
    {
        private readonly ApiDbContext _context; 
        public AddUserEmailHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<AddUserEmailResponseDto> ExecuteAsync(AddUserEmailCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users 
                .Include(u => u.Emails) 
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var exists = await _context.UserEmails
                .AnyAsync(e => e.UserId == user.Id && e.Email == dto.Email, ct);

            if (exists)
            {
                throw new Exception("Ese correo ya está registrado para este usuario"); 
            }

            var newEmail = new UserEmail 
            { 
                Id = Guid.NewGuid(), 
                Email = dto.Email, 
                UserId = user.Id
            };

            await _context.UserEmails.AddAsync(newEmail, ct);
            await _context.SaveChangesAsync(ct);

            return new AddUserEmailResponseDto
            {
                UserId = user.Id, 
                EmailId = newEmail.Id, 
                Message = "Email añadido correctamente"
            };
        }
    }
}