using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Auth.Commands;
using ApiMedialityc.Features.Auth.DTOs;
using ApiMedialityc.Features.Common.Security;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Features.Auth.Handlers
{
    public class LoginHandler
        : CommandHandler<LoginCommand, LoginResponseDto>
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;

        public LoginHandler(ApiDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public override async Task<LoginResponseDto> ExecuteAsync(LoginCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            var user = await _context.Users 
                .FirstOrDefaultAsync(u => u.FullName == dto.FullName, ct); 
            
            if (user == null || !PasswordHasher.Verify(dto.Password, user.Password))
            {
                throw new Exception("Credenciales inválidas");
            }

            if (!user.IsActive)
            {
                throw new Exception("Esta usted inactivo, habla con el administrador para activar su cuenta");
            }

            var token = JwtTokenGenerator.GenerateToken(user.Id, user.FullName, user.Role.ToString(), _config); 

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:AccessTokenMinutes"] ?? "60"))
            };
        }
    }
}