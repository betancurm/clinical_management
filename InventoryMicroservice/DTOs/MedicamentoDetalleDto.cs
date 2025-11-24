using InventoryMicroservice.Models;

namespace InventoryMicroservice.DTOs;

public class MedicamentoDetalleDto : MedicamentoDto
{
    public IEnumerable<LoteDto> Lotes { get; set; } = new List<LoteDto>();
}