using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Models;
using ApiMedialityc.Features.Users.Models;
using ApiMedialityc.Features.Vehicles.Models;
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
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<VehicleInventory> VehicleInventories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             // ---Configuracion Users---
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

             // ---Configuracion Vehicles---
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Plate).IsRequired();
                entity.Property(v => v.Brand).IsRequired();
                entity.Property(v => v.Model).IsRequired();

                entity.Property(v => v.Type)
                    .HasConversion<string>()
                    .IsRequired();
                
                entity.Property(v => v.Price)
                    .HasPrecision(12, 2)
                    .IsRequired();
                
                entity.HasIndex(v => v.Plate).IsUnique();

                entity.HasOne(v => v.VehicleInventory)
                    .WithMany(vi => vi.Vehicles)
                    .HasForeignKey(v => v.VehicleInventoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(v => v.Sales)
                    .WithOne(r => r.Vehicle)
                    .HasForeignKey(r => r.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(v => v.Sales)
                    .WithOne(s => s.Vehicle)
                    .HasForeignKey(s => s.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ---Configuracion VehicleInventory---
            modelBuilder.Entity<VehicleInventory>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Brand).IsRequired();
                entity.Property(v => v.Model).IsRequired();

                entity.Property(v => v.Type)
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(v => v.AvailableQuantity).IsRequired();

                entity.Property(v => v.AvailableQuantity).IsRequired();
                entity.Property(v => v.IsAvailable)
                    .HasComputedColumnSql(@"CASE WHEN ""AvailableQuantity"" > 0 THEN true ELSE false END", stored: true);

                entity.HasMany(vi => vi.Vehicles)
                    .WithOne(v => v.VehicleInventory)
                    .HasForeignKey(v => v.VehicleInventoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

             // ---Configuracion Sales---
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Price).IsRequired();
                entity.Property(s => s.SaleDate).IsRequired();
                entity.Property(s => s.Status)
                    .HasConversion<string>()
                    .IsRequired();

                entity.HasOne(s => s.User)
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Vehicle)
                    .WithMany(v => v.Sales)
                    .HasForeignKey(s => s.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Índice compuesto por VehicleId y UserId para evitar duplicados
                entity.HasIndex(s => new { s.VehicleId, s.UserId })
                    .HasDatabaseName("Sales_VehicleUser");
            });
        }
    }
}