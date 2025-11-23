using PatientManagementMicroservice.Models;
#nullable enable
namespace PatientManagementMicroservice.DTOs;

public class CreatePatientRequest
{
    public PatientDto Patient { get; set; }
    public PatientExtraInfoDto? ExtraInfo { get; set; }
}

public class PatientDto
{
    public string NumeroIdentificacion { get; set; }
    public string NombreCompleto { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public Genero Genero { get; set; }
    public string Direccion { get; set; }
    public string NumeroTelefono { get; set; }
    public string? CorreoElectronico { get; set; }
}

public class PatientExtraInfoDto
{
    public string PrimerNombreContacto { get; set; }
    public string? SegundoNombreContacto { get; set; }
    public string PrimerApellidoContacto { get; set; }
    public string? SegundoApellidoContacto { get; set; }
    public string RelacionConPaciente { get; set; }
    public string NumeroTelefonoEmergencia { get; set; }
    public string NombreCompaniaSeguros { get; set; }
    public string NumeroPoliza { get; set; }
    public bool PolizaActiva { get; set; }
    public DateTime VigenciaPoliza { get; set; }
}