using AuthenticationMicroservice.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationMicroservice;

public static class Seeder
{
    public static void Seed(ApplicationDbContext context)
    {
        var defaultUsers = new List<User>
        {
            new User
            {
                PrimerNombre = "Eduardo",
                PrimerApellido = "cast",
                NumeroCedula = "111111111",
                CorreoElectronico = "juan.perez@yopmail.com",
                NumeroTelefono = "3001111111",
                FechaNacimiento = new DateTime(1985, 5, 15),
                Direccion = "Calle 1 #10-20",
                Rol = Rol.SoporteTecnico
            },
            new User
            {
                PrimerNombre = "Juan",
                PrimerApellido = "Pérez",
                NumeroCedula = "111111111",
                CorreoElectronico = "juan.perez@yopmail.com",
                NumeroTelefono = "3001111111",
                FechaNacimiento = new DateTime(1985, 5, 15),
                Direccion = "Calle 1 #10-20",
                Rol = Rol.Medico
            },
            new User
            {
                PrimerNombre = "María",
                PrimerApellido = "Gómez",
                NumeroCedula = "222222222",
                CorreoElectronico = "maria.gomez@yopmail.com",
                NumeroTelefono = "3002222222",
                FechaNacimiento = new DateTime(1990, 8, 22),
                Direccion = "Carrera 2 #15-30",
                Rol = Rol.Enfermero
            },
            new User
            {
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                NumeroCedula = "333333333",
                CorreoElectronico = "carlos.rodriguez@yopmail.com",
                NumeroTelefono = "3003333333",
                FechaNacimiento = new DateTime(1982, 12, 10),
                Direccion = "Avenida 3 #25-40",
                Rol = Rol.PersonalAdministrativo
            },
            new User
            {
                PrimerNombre = "Ana",
                PrimerApellido = "López",
                NumeroCedula = "444444444",
                CorreoElectronico = "ana.lopez@yopmail.com",
                NumeroTelefono = "3004444444",
                FechaNacimiento = new DateTime(1975, 3, 5),
                Direccion = "Transversal 4 #35-50",
                Rol = Rol.RecursosHumanos
            }
        };

        var passwordHasher = new PasswordHasher<User>();

        foreach (var userTemplate in defaultUsers)
        {
            if (!context.Users.Any(u => u.Rol == userTemplate.Rol))
            {
                string plainPassword = GeneratePassword();
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    PrimerNombre = userTemplate.PrimerNombre,
                    PrimerApellido = userTemplate.PrimerApellido,
                    NumeroCedula = userTemplate.NumeroCedula,
                    CorreoElectronico = userTemplate.CorreoElectronico,
                    NumeroTelefono = userTemplate.NumeroTelefono,
                    FechaNacimiento = userTemplate.FechaNacimiento,
                    Direccion = userTemplate.Direccion,
                    Rol = userTemplate.Rol,
                    NombreUsuario = GenerateUniqueUsername(context, userTemplate),
                    ContrasenaHash = passwordHasher.HashPassword(null, plainPassword)
                };
                context.Users.Add(user);
                Console.WriteLine($"Usuario {user.Rol} creado. Nombre de usuario: {user.NombreUsuario}, Contraseña: {plainPassword}, Correo: {user.CorreoElectronico}");
            }
        }

        context.SaveChanges();
    }

    private static string GenerateUniqueUsername(ApplicationDbContext context, User user)
    {
        string baseUsername = $"{user.PrimerNombre}{user.PrimerApellido}".ToLower();
        if (!context.Users.Any(u => u.NombreUsuario == baseUsername)) return baseUsername;

        if (!string.IsNullOrEmpty(user.SegundoNombre))
        {
            string alt1 = $"{user.SegundoNombre}{user.PrimerApellido}".ToLower();
            if (!context.Users.Any(u => u.NombreUsuario == alt1)) return alt1;
        }

        if (!string.IsNullOrEmpty(user.SegundoApellido))
        {
            string alt2 = $"{user.PrimerNombre}{user.SegundoApellido}".ToLower();
            if (!context.Users.Any(u => u.NombreUsuario == alt2)) return alt2;
        }

        // Agregar 3 dígitos aleatorios
        Random random = new();
        string username;
        do
        {
            username = baseUsername + random.Next(100, 1000);
        } while (context.Users.Any(u => u.NombreUsuario == username));
        return username;
    }

    private static string GeneratePassword()
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
}