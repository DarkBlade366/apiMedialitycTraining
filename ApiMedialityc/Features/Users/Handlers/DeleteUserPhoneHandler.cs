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
    public class DeleteUserPhoneHandler
: CommandHandler<DeleteUserPhoneCommand, DeleteUserPhoneResponseDto>
    {
        private readonly ApiDbContext _context;

        public DeleteUserPhoneHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<DeleteUserPhoneResponseDto> ExecuteAsync(DeleteUserPhoneCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users
                .Include(u => u.Phones) 
                .FirstOrDefaultAsync(u => u.Id == dto.Id, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var phone = user.Phones.FirstOrDefault(p => p.Phone == dto.Phone);

            if (phone == null)
            {
                throw new Exception("Telefono no encontrado"); 
            }

            if (user.Phones.Count <= 1)
            {
                throw new Exception("No se puede eliminar el último telefono del usuario"); 
            }

            _context.UserPhones.Remove(phone);
            await _context.SaveChangesAsync(ct);

            return new DeleteUserPhoneResponseDto 
            { 
                Id = user.Id, 
                Phone = dto.Phone, 
                Message = "Phone eliminado correctamente" 
            };
        }
    }
}