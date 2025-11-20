using UserService.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserService.Validators;

public static class UserValidators
{
    private const string EMAIL_PATTERN = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    private const string USERNAME_PATTERN = @"^[a-zA-Z0-9]{1,15}$";
    private const string PHONE_PATTERN = @"^\d{1,10}$";
    private const string PASSWORD_PATTERN = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$";

    public static List<string> ValidateAll(UserCreateDto userDto)
    {
        var errors = new List<string>();

        errors.AddRange(ValidateNombreCompleto(userDto.NombreCompleto));
        errors.AddRange(ValidateCedula(userDto.Cedula));
        errors.AddRange(ValidateCorreoElectronico(userDto.CorreoElectronico));
        errors.AddRange(ValidateNumeroTelefono(userDto.NumeroTelefono));
        errors.AddRange(ValidateFechaNacimiento(userDto.FechaNacimiento));
        errors.AddRange(ValidateDireccion(userDto.Direccion));
        errors.AddRange(ValidateRol(userDto.Rol));
        errors.AddRange(ValidateNombreUsuario(userDto.NombreUsuario));
        errors.AddRange(ValidateContraseña(userDto.Contraseña));

        return errors;
    }

    public static List<string> ValidateUpdate(UserUpdateDto userDto)
    {
        var errors = new List<string>();

        if (!string.IsNullOrEmpty(userDto.NombreCompleto))
            errors.AddRange(ValidateNombreCompleto(userDto.NombreCompleto));
        
        if (!string.IsNullOrEmpty(userDto.Cedula))
            errors.AddRange(ValidateCedula(userDto.Cedula));
        
        if (!string.IsNullOrEmpty(userDto.CorreoElectronico))
            errors.AddRange(ValidateCorreoElectronico(userDto.CorreoElectronico));
        
        if (!string.IsNullOrEmpty(userDto.NumeroTelefono))
            errors.AddRange(ValidateNumeroTelefono(userDto.NumeroTelefono));
        
        if (userDto.FechaNacimiento.HasValue)
            errors.AddRange(ValidateFechaNacimiento(userDto.FechaNacimiento.Value));
        
        if (!string.IsNullOrEmpty(userDto.Direccion))
            errors.AddRange(ValidateDireccion(userDto.Direccion));
        
        if (!string.IsNullOrEmpty(userDto.Rol))
            errors.AddRange(ValidateRol(userDto.Rol));
        
        if (!string.IsNullOrEmpty(userDto.NombreUsuario))
            errors.AddRange(ValidateNombreUsuario(userDto.NombreUsuario));
        
        if (!string.IsNullOrEmpty(userDto.Contraseña))
            errors.AddRange(ValidateContraseña(userDto.Contraseña));

        return errors;
    }

    public static List<string> ValidateNombreCompleto(string nombreCompleto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(nombreCompleto))
            errors.Add("El nombre completo es requerido.");
        else if (nombreCompleto.Length > 100)
            errors.Add("El nombre completo no puede exceder 100 caracteres.");

        return errors;
    }

    public static List<string> ValidateCedula(string cedula)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(cedula))
            errors.Add("La cédula es requerida.");
        else if (cedula.Length > 15)
            errors.Add("La cédula no puede exceder 15 caracteres.");
        else if (!cedula.All(char.IsDigit))
            errors.Add("La cédula solo puede contener números.");

        return errors;
    }

    public static List<string> ValidateCorreoElectronico(string correoElectronico)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(correoElectronico))
            errors.Add("El correo electrónico es requerido.");
        else if (correoElectronico.Length > 100)
            errors.Add("El correo electrónico no puede exceder 100 caracteres.");
        else if (!Regex.IsMatch(correoElectronico, EMAIL_PATTERN))
            errors.Add("El formato del correo electrónico no es válido.");
        else
        {
            var parts = correoElectronico.Split('@');
            if (parts.Length != 2 || string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]))
                errors.Add("El correo electrónico debe contener un dominio válido.");
            else if (!parts[1].Contains('.'))
                errors.Add("El correo electrónico debe contener un dominio con extensión válida.");
        }

        return errors;
    }

    public static List<string> ValidateNumeroTelefono(string? numeroTelefono)
    {
        var errors = new List<string>();

        if (!string.IsNullOrEmpty(numeroTelefono))
        {
            if (!Regex.IsMatch(numeroTelefono, PHONE_PATTERN))
                errors.Add("El número de teléfono debe contener entre 1 y 10 dígitos.");
        }

        return errors;
    }

    public static List<string> ValidateFechaNacimiento(DateTime fechaNacimiento)
    {
        var errors = new List<string>();

        var hoy = DateTime.Now;
        var edad = hoy.Year - fechaNacimiento.Year;
        if (hoy.Month < fechaNacimiento.Month || (hoy.Month == fechaNacimiento.Month && hoy.Day < fechaNacimiento.Day))
            edad--;

        if (fechaNacimiento > hoy)
            errors.Add("La fecha de nacimiento no puede ser futura.");
        else if (edad > 150)
            errors.Add("La edad no puede exceder 150 años.");
        else if (edad < 0)
            errors.Add("La fecha de nacimiento no es válida.");

        return errors;
    }

    public static List<string> ValidateDireccion(string direccion)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(direccion))
            errors.Add("La dirección es requerida.");
        else if (direccion.Length > 30)
            errors.Add("La dirección no puede exceder 30 caracteres.");

        return errors;
    }

    public static List<string> ValidateRol(string rol)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(rol))
            errors.Add("El rol es requerido.");
        else if (rol.Length > 50)
            errors.Add("El rol no puede exceder 50 caracteres.");

        return errors;
    }

    public static List<string> ValidateNombreUsuario(string nombreUsuario)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(nombreUsuario))
            errors.Add("El nombre de usuario es requerido.");
        else if (nombreUsuario.Length > 15)
            errors.Add("El nombre de usuario no puede exceder 15 caracteres.");
        else if (!Regex.IsMatch(nombreUsuario, USERNAME_PATTERN))
            errors.Add("El nombre de usuario solo puede contener letras y números.");

        return errors;
    }

    public static List<string> ValidateContraseña(string contraseña)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(contraseña))
            errors.Add("La contraseña es requerida.");
        else if (contraseña.Length < 8)
            errors.Add("La contraseña debe tener al menos 8 caracteres.");
        else if (!Regex.IsMatch(contraseña, PASSWORD_PATTERN))
        {
            var missingRequirements = new List<string>();
            if (!contraseña.Any(char.IsUpper)) missingRequirements.Add("una mayúscula");
            if (!contraseña.Any(char.IsLower)) missingRequirements.Add("una minúscula");
            if (!contraseña.Any(char.IsDigit)) missingRequirements.Add("un número");
            if (!contraseña.Any(ch => !char.IsLetterOrDigit(ch))) missingRequirements.Add("un carácter especial");
            
            if (missingRequirements.Any())
                errors.Add($"La contraseña debe incluir: {string.Join(", ", missingRequirements)}.");
        }

        return errors;
    }
}