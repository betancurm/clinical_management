using UserService.DTOs;
using UserService.Models;

namespace UserService.Services;

public interface IUserService
{
    Task<UserDto> CrearUsuarioAsync(UserCreateDto userCreateDto);
    Task<UserDto?> ObtenerUsuarioPorIdAsync(int id);
    Task<IEnumerable<UserDto>> ObtenerTodosLosUsuariosAsync();
    Task<UserDto?> ActualizarUsuarioAsync(int id, UserUpdateDto userUpdateDto);
    Task<bool> EliminarUsuarioAsync(int id);
    Task<UserDto?> ObtenerUsuarioPorCedulaAsync(string cedula);
    Task<UserDto?> ObtenerUsuarioPorNombreUsuarioAsync(string nombreUsuario);
    Task<UserLoginResponseDto> AutenticarAsync(UserLoginDto loginDto);
    Task<IEnumerable<UserDto>> ObtenerUsuariosPorRolAsync(string rol);
    Task<bool> ExisteUsuarioAsync(int id);
    Task<int> ContarUsuariosActivosAsync();
}