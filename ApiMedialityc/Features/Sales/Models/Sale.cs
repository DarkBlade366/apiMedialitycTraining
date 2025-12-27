using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Sales.Enum;
using ApiMedialityc.Features.Users.Models;
using ApiMedialityc.Features.Vehicles.Models;

namespace ApiMedialityc.Features.Sales.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        public decimal Price { get; set; } // Copiado del vehículo al momento de la venta

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public SaleStatus Status { get; set; } = SaleStatus.Pending;

        // Navegación
        public virtual User User { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}