using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace PatientManagementMicroservice.Models;

public class VisitRecord
{
    public Guid Id { get; set; }

    [Required]
    public Guid PatientId { get; set; }

    [Required]
    public DateTime FechaVisita { get; set; } = DateTime.UtcNow;

    public int? PresionArterialSistolica { get; set; }
    public int? PresionArterialDiastolica { get; set; }

    public decimal? Temperatura { get; set; }

    public int? Pulso { get; set; }

    public int? Oxigenacion { get; set; }

    public string? Notas { get; set; }

    // Navegación
    [JsonIgnore]
    public Patient Patient { get; set; }
}