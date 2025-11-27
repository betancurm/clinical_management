using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Services;

public interface IAyudaDiagnosticaService
{
    Task<IEnumerable<AyudaDiagnosticaDto>> GetAllAyudasDiagnosticasAsync();
    Task<AyudaDiagnosticaDto> GetAyudaDiagnosticaByIdAsync(Guid id);
    Task<AyudaDiagnosticaDto> CreateAyudaDiagnosticaAsync(CreateAyudaDiagnosticaDto dto);
    Task<AyudaDiagnosticaDto> UpdateAyudaDiagnosticaAsync(Guid id, UpdateAyudaDiagnosticaDto dto);
    Task DeleteAyudaDiagnosticaAsync(Guid id);
}