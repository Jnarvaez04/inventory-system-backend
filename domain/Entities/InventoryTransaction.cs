using inventarySystem_backend.domain.Enums;
using Microsoft.AspNetCore.WebUtilities;

namespace inventarySystem_backend.domain.Entities;
public class InventoryTransaction
{
        public int Id { get; set; }
        public int Quantity { get; set; }
        public TransactionType Type { get; set; }
        public string? Reason { get; set; } // Ej: "Venta interna", "Proveedor X", "Merma"
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // RELACIONES DE AUDITORÍA

        // 1. Relación con el producto impactado
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // 2. Relación con el Usuario que ejecutó la acción (Seguridad y Trazabilidad)
        public int UserId { get; set; }
        public User? User { get; set; }


}
