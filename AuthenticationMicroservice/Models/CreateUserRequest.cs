using System;

namespace AuthenticationMicroservice.Models;

public class CreateUserRequest
{
    public string PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public string NumeroCedula { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string CorreoElectronico { get; set; }
    public string NumeroTelefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; }
    public Rol Rol { get; set; }
}