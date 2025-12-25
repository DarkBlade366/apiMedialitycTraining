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
    public class GetMyProfileHandler : CommandHandler<GetMyProfileQuery, GetMyProfileResponseDto>
    {
        private readonly ApiDbContext _context;

        public GetMyProfileHandler(ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<GetMyProfileResponseDto> ExecuteAsync(GetMyProfileQuery q, CancellationToken ct)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Id == q.UserId, ct);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            return new GetMyProfileResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                IsActive = user.IsActive,
                Role = user.Role.ToString(),
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
