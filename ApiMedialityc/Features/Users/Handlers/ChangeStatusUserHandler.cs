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
        public class ChangeStatusUserHandler
            : CommandHandler<ChangeStatusUserCommand, ChangeStatusUserResponseDto>
        {
            private readonly ApiDbContext _context;

            public ChangeStatusUserHandler(ApiDbContext context)
            {
                _context = context;
            }

            public override async Task<ChangeStatusUserResponseDto> ExecuteAsync(ChangeStatusUserCommand c, CancellationToken ct)
            {
                var request = c.Request;

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == c.Request.Id, ct);

                if(user == null)
                {
                    return new ChangeStatusUserResponseDto
                    {
                        IsActive = false,
                        Message = "Usuario no encontrado"
                    };
                }

                if (user.IsActive == request.IsActive)
                {
                    return new ChangeStatusUserResponseDto
                    {
                        IsActive = user.IsActive,
                        Message = request.IsActive
                            ? "El usuario ya está activado"
                            : "El usuario ya está desactivado"
                    };
                }

                user.IsActive = request.IsActive;

                await _context.SaveChangesAsync();

                return new ChangeStatusUserResponseDto
                {
                    IsActive = user.IsActive,
                    Message = request.IsActive 
                        ? "Usuario activado con éxito"
                        : "Usuario desactivado con éxito"
                };
            }
        }
    }