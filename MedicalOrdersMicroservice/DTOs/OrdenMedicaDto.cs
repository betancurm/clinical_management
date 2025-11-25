using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.DTOs;

public class OrdenMedicaDto
{
    public string NumeroOrden { get; set; }
    public string CedulaPaciente { get; set; }
    public string CedulaMedico { get; set; }
    public Guid CitaId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public TipoOrden TipoOrden { get; set; }
    public IEnumerable<MedicamentoDetalleDto> Medicamentos { get; set; } = new List<MedicamentoDetalleDto>();
    public IEnumerable<ProcedimientoDetalleDto> Procedimientos { get; set; } = new List<ProcedimientoDetalleDto>();
    public IEnumerable<AyudaDiagnosticaDetalleDto> AyudasDiagnosticas { get; set; } = new List<AyudaDiagnosticaDetalleDto>();
}