using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public class OrdenMedicamentoDetalle
{
    [Key]
    public string NumeroOrden { get; set; }

    [Key]
    public int NumeroItem { get; set; }

    [Required]
    public string NombreMedicamento { get; set; }

    public Guid? IdMedicamento { get; set; }

    [Required]
    public string Dosis { get; set; }

    [Required]
    public string DuracionTratamiento { get; set; }

    public decimal? Costo { get; set; }

    // Navegación
    public OrdenMedica OrdenMedica { get; set; }
}