using HistoriaClinicaMicroservice.Models;

namespace HistoriaClinicaMicroservice.Services;

public interface IHistoriaClinicaService
{
    Task RegistrarAtencionAsync(RegistrarAtencionDto dto);
    Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriaPorPacienteAsync(string cedulaPaciente);
    Task<AtencionHistoriaClinicaDto> ObtenerAtencionPorFechaAsync(string cedulaPaciente, DateTime fechaAtencion);
    Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriaPorRangoFechasAsync(string cedulaPaciente, DateTime fechaInicio, DateTime fechaFin);
    Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriasPorMedicoAsync(string cedulaMedico);
}