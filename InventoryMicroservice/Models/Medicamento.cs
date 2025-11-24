using System.ComponentModel.DataAnnotations;

namespace InventoryMicroservice.Models;

public class Medicamento
{
    public Guid Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Concentracion { get; set; }

    public FormaFarmaceutica? FormaFarmaceutica { get; set; }

    public string? Proveedor { get; set; }

    public bool Activo { get; set; } = true;

    // Navegación 1:N
    public ICollection<MedicamentoLote> Lotes { get; set; } = new List<MedicamentoLote>();
}