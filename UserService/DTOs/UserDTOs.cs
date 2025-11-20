namespace UserService.DTOs;

public class UserCreateDto
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string? NumeroTelefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
}

public class UserUpdateDto
{
    public string? NombreCompleto { get; set; }
    public string? Cedula { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? NumeroTelefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public string? Rol { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Contraseña { get; set; }
    public bool? Activo { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string? NumeroTelefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaUltimaModificacion { get; set; }
    public bool Activo { get; set; }
}

public class UserLoginDto
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
}

public class UserLoginResponseDto
{
    public bool Exito { get; set; }
    public string? Token { get; set; }
    public string? Mensaje { get; set; }
}