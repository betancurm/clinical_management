using InventoryMicroservice.Models;

namespace InventoryMicroservice.DTOs;

public class UpdateMedicamentoDto
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Concentracion { get; set; }
    public FormaFarmaceutica? FormaFarmaceutica { get; set; }
    public string? Proveedor { get; set; }
}