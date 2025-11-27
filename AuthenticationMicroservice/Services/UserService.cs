using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Validations;
using AuthenticationMicroservice.Exceptions;

namespace AuthenticationMicroservice.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public CreateUserResult CreateUser(User user)
    {
        UserValidator.Validate(user);

        // Verificar unicidad de número de cédula
        if (_context.Users.Any(u => u.NumeroCedula == user.NumeroCedula))
            throw new BusinessException("El número de cédula ya existe.");

        // Generar nombre de usuario único
        user.NombreUsuario = GenerateUniqueUsername(user);

        // Generar contraseña
        string plainPassword = GeneratePassword();
        user.ContrasenaHash = _passwordHasher.HashPassword(user, plainPassword);

        user.Id = Guid.NewGuid();
        _context.Users.Add(user);
        _context.SaveChanges();

        return new CreateUserResult { User = user, GeneratedPassword = plainPassword };
    }

    private string GenerateUniqueUsername(User user)
    {
        string baseUsername = $"{user.PrimerNombre}{user.PrimerApellido}".ToLower();
        if (!_context.Users.Any(u => u.NombreUsuario == baseUsername)) return baseUsername;

        if (!string.IsNullOrEmpty(user.SegundoNombre))
        {
            string alt1 = $"{user.SegundoNombre}{user.PrimerApellido}".ToLower();
            if (!_context.Users.Any(u => u.NombreUsuario == alt1)) return alt1;
        }

        if (!string.IsNullOrEmpty(user.SegundoApellido))
        {
            string alt2 = $"{user.PrimerNombre}{user.SegundoApellido}".ToLower();
            if (!_context.Users.Any(u => u.NombreUsuario == alt2)) return alt2;
        }

        // Agregar 3 dígitos aleatorios
        Random random = new();
        string username;
        do
        {
            username = baseUsername + random.Next(100, 1000);
        } while (_context.Users.Any(u => u.NombreUsuario == username));
        return username;
    }

    private string GeneratePassword()
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string special = "!@#$%^&*";
        Random random = new();
        int length = random.Next(8, 13);
        string password = "";
        password += upper[random.Next(upper.Length)];
        password += lower[random.Next(lower.Length)];
        password += digits[random.Next(digits.Length)];
        password += special[random.Next(special.Length)];
        string allChars = upper + lower + digits + special;
        for (int i = 4; i < length; i++)
        {
            password += allChars[random.Next(allChars.Length)];
        }
        return new string(password.OrderBy(c => random.Next()).ToArray());
    }

    public void DeleteUser(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");
        return user;
    }

    public void UpdateUser(string numeroCedula, UpdateUserRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.NumeroCedula == numeroCedula);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");

        // Actualizar campos
        user.PrimerNombre = request.PrimerNombre;
        user.SegundoNombre = request.SegundoNombre;
        user.PrimerApellido = request.PrimerApellido;
        user.SegundoApellido = request.SegundoApellido;
        user.CorreoElectronico = request.CorreoElectronico;
        user.NumeroTelefono = request.NumeroTelefono;
        user.FechaNacimiento = request.FechaNacimiento;
        user.Direccion = request.Direccion;
        user.Rol = request.Rol;

        // Validar el usuario actualizado
        UserValidator.Validate(user);

        // Verificar unicidad de cédula si cambió (pero como es el identificador, no cambia)
        // Si se permite cambiar cédula, agregar validación

        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public User GetUserByNumeroCedula(string numeroCedula)
    {
        var user = _context.Users.FirstOrDefault(u => u.NumeroCedula == numeroCedula);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");
        return user;
    }

    public void DeleteUserByNumeroCedula(string numeroCedula)
    {
        var user = _context.Users.FirstOrDefault(u => u.NumeroCedula == numeroCedula);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}