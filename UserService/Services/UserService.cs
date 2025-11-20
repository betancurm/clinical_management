using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTOs;
using UserService.Exceptions;
using UserService.Models;
using UserService.Validators;

namespace UserService.Services;

public class UserService : IUserService
{
    private readonly UserDbContext _context;

    public UserService(UserDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> CrearUsuarioAsync(UserCreateDto userCreateDto)
    {
        // Validar datos usando los validadores
        var validationErrors = UserValidators.ValidateAll(userCreateDto);
        if (validationErrors.Any())
        {
            throw new ValidationException("Errores de validación encontrados.", validationErrors);
        }

        // Verificar duplicados
        if (await ExisteCedulaAsync(userCreateDto.Cedula))
        {
            throw new DuplicateCedulaException(userCreateDto.Cedula);
        }

        if (await ExisteNombreUsuarioAsync(userCreateDto.NombreUsuario))
        {
            throw new DuplicateUsernameException(userCreateDto.NombreUsuario);
        }

        // Crear usuario usando el patrón Builder
        var user = User.Builder.Crear()
            .ConNombreCompleto(userCreateDto.NombreCompleto)
            .ConCedula(userCreateDto.Cedula)
            .ConCorreoElectronico(userCreateDto.CorreoElectronico)
            .ConNumeroTelefono(userCreateDto.NumeroTelefono)
            .ConFechaNacimiento(userCreateDto.FechaNacimiento)
            .ConDireccion(userCreateDto.Direccion)
            .ConRol(userCreateDto.Rol)
            .ConNombreUsuario(userCreateDto.NombreUsuario)
            .ConContraseña(userCreateDto.Contraseña)
            .Construir();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return MapearADto(user);
    }

    public async Task<UserDto?> ObtenerUsuarioPorIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user != null ? MapearADto(user) : null;
    }

    public async Task<IEnumerable<UserDto>> ObtenerTodosLosUsuariosAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(MapearADto);
    }

    public async Task<UserDto?> ActualizarUsuarioAsync(int id, UserUpdateDto userUpdateDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        // Validar datos si están presentes
        var validationErrors = UserValidators.ValidateUpdate(userUpdateDto);
        if (validationErrors.Any())
        {
            throw new ValidationException("Errores de validación encontrados.", validationErrors);
        }

        // Verificar duplicados para campos únicos si están siendo actualizados
        if (!string.IsNullOrEmpty(userUpdateDto.Cedula) && 
            userUpdateDto.Cedula != user.Cedula && 
            await ExisteCedulaAsync(userUpdateDto.Cedula))
        {
            throw new DuplicateCedulaException(userUpdateDto.Cedula);
        }

        if (!string.IsNullOrEmpty(userUpdateDto.NombreUsuario) && 
            userUpdateDto.NombreUsuario != user.NombreUsuario && 
            await ExisteNombreUsuarioAsync(userUpdateDto.NombreUsuario))
        {
            throw new DuplicateUsernameException(userUpdateDto.NombreUsuario);
        }

        // Actualizar campos usando el método del modelo
        user.Actualizar(
            nombreCompleto: userUpdateDto.NombreCompleto,
            cedula: userUpdateDto.Cedula,
            correoElectronico: userUpdateDto.CorreoElectronico,
            numeroTelefono: userUpdateDto.NumeroTelefono,
            fechaNacimiento: userUpdateDto.FechaNacimiento,
            direccion: userUpdateDto.Direccion,
            rol: userUpdateDto.Rol,
            nombreUsuario: userUpdateDto.NombreUsuario,
            contraseña: userUpdateDto.Contraseña,
            activo: userUpdateDto.Activo
        );

        await _context.SaveChangesAsync();

        return MapearADto(user);
    }

    public async Task<bool> EliminarUsuarioAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto?> ObtenerUsuarioPorCedulaAsync(string cedula)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Cedula == cedula);
        return user != null ? MapearADto(user) : null;
    }

    public async Task<UserDto?> ObtenerUsuarioPorNombreUsuarioAsync(string nombreUsuario)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        return user != null ? MapearADto(user) : null;
    }

    public async Task<UserLoginResponseDto> AutenticarAsync(UserLoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => 
            u.NombreUsuario == loginDto.NombreUsuario && u.Activo);

        if (user == null || user.Contraseña != loginDto.Contraseña)
        {
            return new UserLoginResponseDto
            {
                Exito = false,
                Mensaje = "Credenciales inválidas."
            };
        }

        // En un entorno real, aquí generarías un token JWT
        return new UserLoginResponseDto
        {
            Exito = true,
            Token = $"token-{user.Id}-{DateTime.Now.Ticks}",
            Mensaje = "Autenticación exitosa."
        };
    }

    public async Task<IEnumerable<UserDto>> ObtenerUsuariosPorRolAsync(string rol)
    {
        var users = await _context.Users.Where(u => u.Rol == rol && u.Activo).ToListAsync();
        return users.Select(MapearADto);
    }

    public async Task<bool> ExisteUsuarioAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<int> ContarUsuariosActivosAsync()
    {
        return await _context.Users.CountAsync(u => u.Activo);
    }

    // Métodos auxiliares
    private async Task<bool> ExisteCedulaAsync(string cedula)
    {
        return await _context.Users.AnyAsync(u => u.Cedula == cedula);
    }

    private async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
    {
        return await _context.Users.AnyAsync(u => u.NombreUsuario == nombreUsuario);
    }

    private static UserDto MapearADto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            NombreCompleto = user.NombreCompleto,
            Cedula = user.Cedula,
            CorreoElectronico = user.CorreoElectronico,
            NumeroTelefono = user.NumeroTelefono,
            FechaNacimiento = user.FechaNacimiento,
            Direccion = user.Direccion,
            Rol = user.Rol,
            NombreUsuario = user.NombreUsuario,
            FechaCreacion = user.FechaCreacion,
            FechaUltimaModificacion = user.FechaUltimaModificacion,
            Activo = user.Activo
        };
    }
}