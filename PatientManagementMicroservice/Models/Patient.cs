using System.ComponentModel.DataAnnotations;

namespace PatientManagementMicroservice.Models;

public class Patient
{
    public Guid Id { get; set; }

    [Required]
    public string NumeroIdentificacion { get; set; }

    [Required]
    public string NombreCompleto { get; set; }

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [Required]
    public Genero Genero { get; set; }

    [Required]
    public string Direccion { get; set; }

    [Required]
    [RegularExpression(@"^\d{10}$")]
    public string NumeroTelefono { get; set; }

    [EmailAddress]
    public string? CorreoElectronico { get; set; }

    // Navegación 1:1
    public PatientExtraInfo? ExtraInfo { get; set; }

    // Navegación 1:N
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<VisitRecord> VisitRecords { get; set; } = new List<VisitRecord>();
}

public enum Genero
{
    Masculino,
    Femenino
}