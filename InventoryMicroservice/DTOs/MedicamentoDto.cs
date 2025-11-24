using InventoryMicroservice.Models;

namespace InventoryMicroservice.DTOs;

public class MedicamentoDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Concentracion { get; set; }
    public FormaFarmaceutica? FormaFarmaceutica { get; set; }
    public string? Proveedor { get; set; }
    public bool Activo { get; set; }
    public int StockTotal { get; set; }
}