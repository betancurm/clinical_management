using InventoryMicroservice.DTOs;

namespace InventoryMicroservice.Services;

public interface IMedicamentoLoteService
{
    Task<LoteDto> CreateAsync(Guid medicamentoId, CreateLoteDto dto);
    Task<IEnumerable<LoteDto>> GetByMedicamentoAsync(Guid medicamentoId);
    Task<LoteDto> GetByIdAsync(Guid medicamentoId, Guid loteId);
    Task<LoteDto> UpdateAsync(Guid medicamentoId, Guid loteId, UpdateLoteDto dto);
    Task<LoteDto> UpdateCantidadAsync(Guid medicamentoId, Guid loteId, ActualizarCantidadLoteDto dto);
    Task DeleteAsync(Guid medicamentoId, Guid loteId);
}