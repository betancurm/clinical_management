using System;

namespace AuthenticationMicroservice.Models;

public enum TipoDocumento
{
    Cedula,
    Pasaporte,
    CedulaExtranjeria
}

public enum Rol
{
    Medico,
    Enfermero,
    PersonalAdministrativo,
    RecursosHumanos
}

public class User
{
    public Guid Id { get; set; }
    public string PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public string NombreCompleto => $"{PrimerNombre} {(SegundoNombre != null ? SegundoNombre + " " : "")}{PrimerApellido}{(SegundoApellido != null ? " " + SegundoApellido : "")}".Trim();
    public string NumeroCedula { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string CorreoElectronico { get; set; }
    public string NumeroTelefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public Rol Rol { get; set; }
    public string NombreUsuario { get; set; }
    public string ContrasenaHash { get; set; }
}