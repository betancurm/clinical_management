using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace InventoryMicroservice.Models;

public class MedicamentoLote
{
    public Guid Id { get; set; }

    [Required]
    public Guid MedicamentoId { get; set; }

    [Required]
    public string Lote { get; set; }

    [Required]
    public DateTime FechaFabricacion { get; set; }

    [Required]
    public DateTime FechaExpiracion { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int CantidadDisponible { get; set; }

    public bool Activo { get; set; } = true;

    // Navegación
    [JsonIgnore]
    public Medicamento Medicamento { get; set; }
}