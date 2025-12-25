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
    public class AddUserPhoneHandler
        : CommandHandler<AddUserPhoneCommand, AddUserPhoneResponseDto>
    {
        private readonly ApiDbContext _context; 
        public AddUserPhoneHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<AddUserPhoneResponseDto> ExecuteAsync(AddUserPhoneCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users 
                .Include(u => u.Phones) 
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var exists = await _context.UserPhones
                .AnyAsync(p => p.UserId == user.Id && p.Phone == dto.Phone, ct);

            if (exists)
            {
                throw new Exception("Ese telefono ya está registrado para este usuario"); 
            }

            var newPhone = new UserPhone
            { 
                Id = Guid.NewGuid(), 
                Phone = dto.Phone, 
                UserId = user.Id
            };

            await _context.UserPhones.AddAsync(newPhone, ct);
            await _context.SaveChangesAsync(ct);

            return new AddUserPhoneResponseDto
            {
                UserId = user.Id, 
                PhoneId = newPhone.Id, 
                Message = "Telefono añadido correctamente"
            };
        }
    }
}