using MedicalOrdersMicroservice.DTOs;

using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Services;

public interface IOrdenMedicaService
{
    Task<OrdenMedicaDto> CreateAsync(OrdenMedica orden);
    Task<OrdenMedicaDto> GetByNumeroOrdenAsync(string numeroOrden);
    Task<IEnumerable<OrdenMedicaDto>> GetByPacienteAsync(string cedulaPaciente);
    Task<IEnumerable<OrdenMedicaDto>> GetByMedicoAsync(string cedulaMedico);
    Task<IEnumerable<OrdenMedicaDto>> GetByCitaAsync(Guid citaId);
    Task<IEnumerable<OrdenMedicaDto>> GetBatchAsync(IEnumerable<string> numeroOrdenes);
}