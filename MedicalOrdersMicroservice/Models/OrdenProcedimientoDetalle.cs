using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public class OrdenProcedimientoDetalle
{
    [Key]
    public string NumeroOrden { get; set; }

    [Key]
    public int NumeroItem { get; set; }

    [Required]
    public string NombreProcedimiento { get; set; }

    public Guid? IdProcedimiento { get; set; }

    [Required]
    public int NumeroVeces { get; set; }

    [Required]
    public string Frecuencia { get; set; }

    public decimal? Costo { get; set; }

    public bool RequiereEspecialista { get; set; }

    public Guid? IdTipoEspecialidad { get; set; }

    // Navegación
    public OrdenMedica OrdenMedica { get; set; }
}