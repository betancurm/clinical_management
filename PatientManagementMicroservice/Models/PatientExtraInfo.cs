using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PatientManagementMicroservice.Models;

public class PatientExtraInfo
{
    public Guid PatientId { get; set; } // Clave primaria y foránea

    // Contacto de emergencia
    [Required]
    public string PrimerNombreContacto { get; set; }

    public string? SegundoNombreContacto { get; set; }

    [Required]
    public string PrimerApellidoContacto { get; set; }

    public string? SegundoApellidoContacto { get; set; }

    [Required]
    public string RelacionConPaciente { get; set; }

    [Required]
    [RegularExpression(@"^\d{10}$")]
    public string NumeroTelefonoEmergencia { get; set; }

    // Seguro médico
    [Required]
    public string NombreCompaniaSeguros { get; set; }

    [Required]
    public string NumeroPoliza { get; set; }

    public bool PolizaActiva { get; set; }

    [Required]
    public DateTime VigenciaPoliza { get; set; }

    // Navegación inversa
    [JsonIgnore] 
    public Patient Patient { get; set; }
}