using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace PatientManagementMicroservice.Models;

public class Appointment
{
    public Guid Id { get; set; }

    [Required]
    public Guid PatientId { get; set; }

    [Required]
    public DateTime FechaHoraCita { get; set; }

    [Required]
    public string Motivo { get; set; }

    public EstadoCita Estado { get; set; } = EstadoCita.Programada;

    // Navegación inversa
    [JsonIgnore]
    public Patient Patient { get; set; }
}

public enum EstadoCita
{
    Programada,
    Cancelada,
    Completada
}