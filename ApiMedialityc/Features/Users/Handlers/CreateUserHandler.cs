using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common.Security;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Enum;
using ApiMedialityc.Features.Users.Models;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class CreateUserHandler
        : CommandHandler<CreateUserCommand, CreateUserResponseDto>
    {
        private readonly ApiDbContext _context;

        public CreateUserHandler (ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<CreateUserResponseDto> ExecuteAsync(CreateUserCommand c, CancellationToken ct)
        {
            var dto = c.Request;

            //Crear usuario
            var user = new User{
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                IsActive = true,
                Password = PasswordHasher.Hash(dto.Password),
                Role = Role.User
            };

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

            //Agregar a DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new CreateUserResponseDto
            {
                Id = user.Id,
                Message = "Su Usuario fue creado con exito"
            };
        }
    }
}