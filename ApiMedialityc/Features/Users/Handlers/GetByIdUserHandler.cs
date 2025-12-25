using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Queries;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class GetByIdUserHandler
        : CommandHandler<GetByIdUserQuery, GetByIdUserResponseDto>
    {
        private readonly ApiDbContext _context;

        public GetByIdUserHandler (ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<GetByIdUserResponseDto> ExecuteAsync(GetByIdUserQuery query, CancellationToken ct)
        {
            var id = query.Request.Id;

            var user = await _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Id == id, ct);

            if (user is null)
                ThrowError("Usuario no encontrado");

            return new GetByIdUserResponseDto
            {
                FullName = user.FullName,
                Emails = user.Emails
                    .Select(e => new UserEmailDto
                    { 
                        Email = e.Email 
                    }).ToList(),
                Phones = user.Phones
                    .Select(p => new UserPhoneDto
                    { 
                        Phone = p.Phone 
                    }).ToList()
            };
        }
    }
}