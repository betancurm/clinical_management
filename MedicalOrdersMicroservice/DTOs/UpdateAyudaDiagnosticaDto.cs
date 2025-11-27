using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.DTOs;

public class UpdateAyudaDiagnosticaDto
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public TipoAyudaDiagnostica Tipo { get; set; }
    public decimal Costo { get; set; }
    public bool RequierePreparacion { get; set; }
    public string? InstruccionesPreparacion { get; set; }
    public bool Activo { get; set; }
}