using System.Text.RegularExpressions;
using AuthenticationMicroservice.Exceptions;
using AuthenticationMicroservice.Models;

namespace AuthenticationMicroservice.Validations;

public static class UserValidator
{
    public static void Validate(User user)
    {
        if (string.IsNullOrWhiteSpace(user.PrimerNombre)) throw new ValidationException("PrimerNombre es requerido.");
        if (string.IsNullOrWhiteSpace(user.PrimerApellido)) throw new ValidationException("PrimerApellido es requerido.");

        // Número de cédula
        if (string.IsNullOrWhiteSpace(user.NumeroCedula)) throw new ValidationException("NumeroCedula es requerido.");
        if (user.TipoDocumento == TipoDocumento.Cedula && !Regex.IsMatch(user.NumeroCedula, @"^\d+$")) throw new ValidationException("NumeroCedula debe contener solo dígitos para tipo Cédula.");
        if (user.TipoDocumento != TipoDocumento.Cedula && !Regex.IsMatch(user.NumeroCedula, @"^[a-zA-Z0-9]+$")) throw new ValidationException("NumeroCedula debe contener solo letras y números para tipos distintos de Cédula.");

        // Correo electrónico
        if (string.IsNullOrWhiteSpace(user.CorreoElectronico)) throw new ValidationException("CorreoElectronico es requerido.");
        if (!Regex.IsMatch(user.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) throw new ValidationException("CorreoElectronico no tiene formato válido.");

        // Número de teléfono
        if (string.IsNullOrWhiteSpace(user.NumeroTelefono)) throw new ValidationException("NumeroTelefono es requerido.");
        if (!Regex.IsMatch(user.NumeroTelefono, @"^\d{1,10}$")) throw new ValidationException("NumeroTelefono debe contener entre 1 y 10 dígitos.");

        // Fecha de nacimiento
        if (user.FechaNacimiento > DateTime.Now) throw new ValidationException("FechaNacimiento no puede ser futura.");
        int age = DateTime.Now.Year - user.FechaNacimiento.Year;
        if (user.FechaNacimiento > DateTime.Now.AddYears(-age)) age--;
        if (age > 150) throw new ValidationException("Edad máxima permitida es 150 años.");

        // Dirección
        if (string.IsNullOrWhiteSpace(user.Direccion)) throw new ValidationException("Direccion es requerida.");
        if (user.Direccion.Length > 30) throw new ValidationException("Direccion no puede exceder 30 caracteres.");

        // Rol
        if (!Enum.IsDefined(typeof(Rol), user.Rol)) throw new ValidationException("Rol no es válido.");
    }
}