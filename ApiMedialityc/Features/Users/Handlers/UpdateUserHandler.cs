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
    public class UpdateUserHandler
        : CommandHandler<UpdateUserCommand, UpdateUserResponseDto>
    {
        private readonly ApiDbContext _context;
        public UpdateUserHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<UpdateUserResponseDto> ExecuteAsync(UpdateUserCommand req, CancellationToken ct)
        {
            var dto= req.Request;

            var user = await _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            user.FullName = dto.FullName;
            user.Emails.Clear();
            user.Phones.Clear();

            //Emails
            user.Emails = dto.Emails
                .Select(e => new UserEmail
                {
                    Email = e.Email,
                    UserId = user.Id
                })
                .ToList();

            //Phones
            user.Phones = dto.Phones
                .Select(p=>new UserPhone
                {
                    Phone = p.Phone,
                    UserId = user.Id
                })
                .ToList();

            await _context.SaveChangesAsync(ct);
            
            return new UpdateUserResponseDto
            {
                Id = user.Id,
                Message = "Usuario actualizado correctamente"
            };
        }
    }
}