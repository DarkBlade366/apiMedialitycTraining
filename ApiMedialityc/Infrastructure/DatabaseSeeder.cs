using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// ApiMedialityc/Infrastructure/DatabaseSeeder.cs
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Models;
using ApiMedialityc.Features.Users.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApiMedialityc.Features.Common.Security;

namespace ApiMedialityc.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseSeeder");

            // 1) Aplicar migraciones pendientes
            await context.Database.MigrateAsync();

            // 2) Si no hay usuarios, crear SuperAdmin inicial
            if (!await context.Users.AnyAsync())
            {
                var superAdmin = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Super Admin",
                    Password = PasswordHasher.Hash("Admin123*"),
                    Role = Role.Admin,
                    IsActive = true,
                    MustChangePassword = true // forzar cambio en primer login
                };

                context.Users.Add(superAdmin);
                await context.SaveChangesAsync();

                logger.LogInformation("SuperAdmin creado: {Id}", superAdmin.Id);
            }
        }
    }
}
