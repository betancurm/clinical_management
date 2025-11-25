using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public class OrdenMedica
{
    [Key]
    [MaxLength(6)]
    public string NumeroOrden { get; set; }

    [Required]
    public string CedulaPaciente { get; set; }

    [Required]
    [MaxLength(10)]
    public string CedulaMedico { get; set; }

    [Required]
    public Guid CitaId { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [Required]
    public TipoOrden TipoOrden { get; set; }

    // Navegación
    public ICollection<OrdenMedicamentoDetalle> Medicamentos { get; set; } = new List<OrdenMedicamentoDetalle>();
    public ICollection<OrdenProcedimientoDetalle> Procedimientos { get; set; } = new List<OrdenProcedimientoDetalle>();
    public ICollection<OrdenAyudaDiagnosticaDetalle> AyudasDiagnosticas { get; set; } = new List<OrdenAyudaDiagnosticaDetalle>();
}