using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Commands;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Enum;
using ApiMedialityc.Features.Users.Models;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class CreateUserHandler
    {
        private readonly ApiDbContext _context;

        public CreateUserHandler (ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateUserResponseDto> HandlerAsync(CreateUserCommand c)
        {
            var dto = c._request;

            //Crear usuario
            var user = new User{
                id = Guid.NewGuid(),
                fullName = dto.fullName,
                isActive = true,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                rol = Rol.User
            };

            //Emails
            user.emails = dto.emails
                .Select(e => new UserEmail
                {
                    email = e.email,
                    userId = user.id
                })
                .ToList();

            //Phones
            user.phones = dto.phones
                .Select(p=>new UserPhone
                {
                    phone = p.phone,
                    userId = user.id
                })
                .ToList();

            //Agregar a DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new CreateUserResponseDto
            {
                id = user.id,
                message = "Su Usuario fue creado con exito"
            };
        } 
    }
}