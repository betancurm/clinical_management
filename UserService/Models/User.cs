using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models;

public class User
{
    [Key]
    public int Id { get; private set; }

    [Required]
    [StringLength(100)]
    public string NombreCompleto { get; private set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string Cedula { get; private set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string CorreoElectronico { get; private set; } = string.Empty;

    [StringLength(10)]
    public string? NumeroTelefono { get; private set; }

    [Required]
    public DateTime FechaNacimiento { get; private set; }

    [Required]
    [StringLength(30)]
    public string Direccion { get; private set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Rol { get; private set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string NombreUsuario { get; private set; } = string.Empty;

    [Required]
    public string Contraseña { get; private set; } = string.Empty;

    public DateTime FechaCreacion { get; private set; }

    public DateTime? FechaUltimaModificacion { get; private set; }

    public bool Activo { get; private set; } = true;

    // Constructor privado para EF Core
    private User() { }

    // Constructor interno para el Builder
    internal User(
        string nombreCompleto,
        string cedula,
        string correoElectronico,
        string? numeroTelefono,
        DateTime fechaNacimiento,
        string direccion,
        string rol,
        string nombreUsuario,
        string contraseña)
    {
        NombreCompleto = nombreCompleto;
        Cedula = cedula;
        CorreoElectronico = correoElectronico;
        NumeroTelefono = numeroTelefono;
        FechaNacimiento = fechaNacimiento;
        Direccion = direccion;
        Rol = rol;
        NombreUsuario = nombreUsuario;
        Contraseña = contraseña;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = DateTime.Now;
        Activo = true;
    }

    public static class Builder
    {
        public static UserBuilder Crear()
        {
            return new UserBuilder();
        }
    }

    // Métodos de actualización para mantener encapsulación
    public void Actualizar(string? nombreCompleto = null, string? cedula = null, 
        string? correoElectronico = null, string? numeroTelefono = null,
        DateTime? fechaNacimiento = null, string? direccion = null,
        string? rol = null, string? nombreUsuario = null, 
        string? contraseña = null, bool? activo = null)
    {
        if (!string.IsNullOrEmpty(nombreCompleto)) NombreCompleto = nombreCompleto;
        if (!string.IsNullOrEmpty(cedula)) Cedula = cedula;
        if (!string.IsNullOrEmpty(correoElectronico)) CorreoElectronico = correoElectronico;
        if (!string.IsNullOrEmpty(numeroTelefono)) NumeroTelefono = numeroTelefono;
        if (fechaNacimiento.HasValue) FechaNacimiento = fechaNacimiento.Value;
        if (!string.IsNullOrEmpty(direccion)) Direccion = direccion;
        if (!string.IsNullOrEmpty(rol)) Rol = rol;
        if (!string.IsNullOrEmpty(nombreUsuario)) NombreUsuario = nombreUsuario;
        if (!string.IsNullOrEmpty(contraseña)) Contraseña = contraseña;
        if (activo.HasValue) Activo = activo.Value;
        
        FechaUltimaModificacion = DateTime.Now;
    }
}

public class UserBuilder
{
    private string _nombreCompleto = string.Empty;
    private string _cedula = string.Empty;
    private string _correoElectronico = string.Empty;
    private string? _numeroTelefono;
    private DateTime _fechaNacimiento;
    private string _direccion = string.Empty;
    private string _rol = string.Empty;
    private string _nombreUsuario = string.Empty;
    private string _contraseña = string.Empty;

    public UserBuilder ConNombreCompleto(string nombreCompleto)
    {
        _nombreCompleto = nombreCompleto;
        return this;
    }

    public UserBuilder ConCedula(string cedula)
    {
        _cedula = cedula;
        return this;
    }

    public UserBuilder ConCorreoElectronico(string correoElectronico)
    {
        _correoElectronico = correoElectronico;
        return this;
    }

    public UserBuilder ConNumeroTelefono(string? numeroTelefono)
    {
        _numeroTelefono = numeroTelefono;
        return this;
    }

    public UserBuilder ConFechaNacimiento(DateTime fechaNacimiento)
    {
        _fechaNacimiento = fechaNacimiento;
        return this;
    }

    public UserBuilder ConDireccion(string direccion)
    {
        _direccion = direccion;
        return this;
    }

    public UserBuilder ConRol(string rol)
    {
        _rol = rol;
        return this;
    }

    public UserBuilder ConNombreUsuario(string nombreUsuario)
    {
        _nombreUsuario = nombreUsuario;
        return this;
    }

    public UserBuilder ConContraseña(string contraseña)
    {
        _contraseña = contraseña;
        return this;
    }

    public User Construir()
    {
        return new User(
            _nombreCompleto,
            _cedula,
            _correoElectronico,
            _numeroTelefono,
            _fechaNacimiento,
            _direccion,
            _rol,
            _nombreUsuario,
            _contraseña
        );
    }
}