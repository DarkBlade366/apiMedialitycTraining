using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Queries;
using Microsoft.EntityFrameworkCore;
using ApiMedialityc.Features.Users.Models;
using System.Text.RegularExpressions;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Handlers
{
    public class GetUsersHandler
        : CommandHandler<GetUsersQuery, PagedResponse<GetUsersResponseDto>>
    {
        private readonly ApiDbContext _context;

        public GetUsersHandler (ApiDbContext context)
        {
            _context = context;
        }

        public override async Task<PagedResponse<GetUsersResponseDto>> ExecuteAsync(GetUsersQuery query, CancellationToken ct)
        {
            var req = query.Request;

            var usersQuery = _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .AsQueryable();

                if (!string.IsNullOrEmpty(req.FullName))
                {
                    usersQuery = usersQuery.Where(u => u.FullName.Contains(req.FullName));
                }

                if (req.IsActive.HasValue)
                {
                    usersQuery = usersQuery.Where(u => u.IsActive == req.IsActive.Value);
                }

                var totalItemsDb = await usersQuery.CountAsync();

                var users = await usersQuery
                    .Skip((req.Page - 1) * req.PageSize)  // Salta los elementos previos según la página actual
                    .Take(req.PageSize)                  // Toma solo la cantidad de elementos que caben en la página
                    .ToListAsync();

            return new PagedResponse<GetUsersResponseDto>
            {
                Items = users.Select(u => new GetUsersResponseDto{
                    Id = u.Id,
                    FullName = u.FullName,
                    IsActive = u.IsActive,
                    Role = u.Role,
                    Emails = u.Emails.Select(e => new UserEmailDto{
                        Email = e.Email
                    }).ToList(),
                    Phones = u.Phones.Select(p => new UserPhoneDto{
                        Phone = p.Phone
                    }).ToList(),
                }).ToList(),
                Page = req.Page,
                PageSize = req.PageSize,
                TotalItems = totalItemsDb,
                TotalPages = (int) Math.Ceiling((double)totalItemsDb / (double) req.PageSize)
            };
        }
    }
}