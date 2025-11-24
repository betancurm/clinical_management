using InventoryMicroservice.DTOs;

namespace InventoryMicroservice.Services;

public interface IMedicamentoService
{
    Task<MedicamentoDto> CreateAsync(CreateMedicamentoDto dto);
    Task<IEnumerable<MedicamentoDto>> GetAllAsync(MedicamentoFilter filter);
    Task<MedicamentoDetalleDto> GetByIdAsync(Guid id);
    Task<MedicamentoDto> UpdateAsync(Guid id, UpdateMedicamentoDto dto);
    Task DeleteAsync(Guid id);
}