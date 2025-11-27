using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public enum TipoAyudaDiagnostica
{
    RayosX,
    Ecografia,
    Tomografia,
    ResonanciaMagnetica
}

public class AyudaDiagnostica
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }

    public TipoAyudaDiagnostica Tipo { get; set; }

    public decimal Costo { get; set; }

    public bool RequierePreparacion { get; set; } = false;

    public string? InstruccionesPreparacion { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}