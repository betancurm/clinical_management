using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Services;

public interface IProcedimientoService
{
    Task<IEnumerable<ProcedimientoDto>> GetAllProcedimientosAsync();
    Task<ProcedimientoDto> GetProcedimientoByIdAsync(Guid id);
    Task<ProcedimientoDto> CreateProcedimientoAsync(CreateProcedimientoDto dto);
    Task<ProcedimientoDto> UpdateProcedimientoAsync(Guid id, UpdateProcedimientoDto dto);
    Task DeleteProcedimientoAsync(Guid id);
}