namespace InventoryMicroservice.DTOs;

public class UpdateLoteDto
{
    public string Lote { get; set; }
    public DateTime FechaFabricacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int CantidadDisponible { get; set; }
}