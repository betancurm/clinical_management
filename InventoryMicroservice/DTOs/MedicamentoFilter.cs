using InventoryMicroservice.Models;

namespace InventoryMicroservice.DTOs;

public class MedicamentoFilter
{
    public string? Nombre { get; set; }
    public string? Proveedor { get; set; }
    public string? Concentracion { get; set; }
    public FormaFarmaceutica? FormaFarmaceutica { get; set; }
    public bool? Activo { get; set; }
}