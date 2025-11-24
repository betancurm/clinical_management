namespace InventoryMicroservice.DTOs;

public class LoteDto
{
    public Guid Id { get; set; }
    public Guid MedicamentoId { get; set; }
    public string Lote { get; set; }
    public DateTime FechaFabricacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int CantidadDisponible { get; set; }
    public bool Activo { get; set; }
    public bool Expirado { get; set; }
}