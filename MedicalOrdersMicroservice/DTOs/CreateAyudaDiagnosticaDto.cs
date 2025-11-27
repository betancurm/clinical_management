using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.DTOs;

public class CreateAyudaDiagnosticaDto
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public TipoAyudaDiagnostica Tipo { get; set; }
    public decimal Costo { get; set; }
    public bool RequierePreparacion { get; set; } = false;
    public string? InstruccionesPreparacion { get; set; }
    public bool Activo { get; set; } = true;
}