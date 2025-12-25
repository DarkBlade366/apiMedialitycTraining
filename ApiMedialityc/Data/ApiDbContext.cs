using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext (DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        //Add DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<UserPhone> UserPhones {get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.IsActive).IsRequired();
                entity.Property(u => u.Role).IsRequired()
                    .HasConversion<string>();
                entity.HasMany(u => u.Emails)      
                    .WithOne(e => e.User)        
                    .HasForeignKey(e => e.UserId) 
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.Phones)
                    .WithOne(p => p.User)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<UserEmail>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Email).IsRequired();
                });

                modelBuilder.Entity<UserPhone>(entity =>
                {
                    entity.HasKey(p => p.Id);
                    entity.Property(p => p.Phone).IsRequired();
                });
            });
        }
    }
}