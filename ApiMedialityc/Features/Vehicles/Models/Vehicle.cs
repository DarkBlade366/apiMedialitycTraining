using ApiMedialityc.Features.Sales.Models;
using ApiMedialityc.Features.Vehicles.Enum;

namespace ApiMedialityc.Features.Vehicles.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Plate { get; set; } = string.Empty; // unico
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public VehicleType Type { get; set; } = VehicleType.Other;
        public Guid VehicleInventoryId { get; set; }  // Clave foránea al Inventario
        public bool IsSold { get; set; } = false;

        //No es necesaria, la dejo por si se quiere un historial detallado de ventas
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        // Navegación inversa obligatoria, EF la llenará
        public virtual VehicleInventory VehicleInventory { get; set; }  = null!;
    }
}