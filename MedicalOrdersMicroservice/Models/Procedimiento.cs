using System.ComponentModel.DataAnnotations;

namespace MedicalOrdersMicroservice.Models;

public class Procedimiento
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }

    public int DuracionEstimadaMinutos { get; set; }

    public decimal Costo { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}