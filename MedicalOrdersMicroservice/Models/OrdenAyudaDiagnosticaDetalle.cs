using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public class OrdenAyudaDiagnosticaDetalle
{
    [Key]
    public string NumeroOrden { get; set; }

    [Key]
    public int NumeroItem { get; set; }

    public Guid AyudaDiagnosticaId { get; set; }

    [Required]
    public int Cantidad { get; set; }

    public decimal? Costo { get; set; }

    public bool RequiereEspecialista { get; set; }

    public Guid? IdTipoEspecialidad { get; set; }

    // Navegación
    public OrdenMedica OrdenMedica { get; set; }
    public AyudaDiagnostica AyudaDiagnostica { get; set; }
}