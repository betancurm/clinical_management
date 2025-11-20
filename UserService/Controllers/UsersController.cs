using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Services;
using UserService.Exceptions;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        try
        {
            var users = await _userService.ObtenerTodosLosUsuariosAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        try
        {
            var user = await _userService.ObtenerUsuarioPorIdAsync(id);
            if (user == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound($"Usuario con ID {id} no encontrado.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuario con ID {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // GET: api/Users/cedula/{cedula}
    [HttpGet("cedula/{cedula}")]
    public async Task<ActionResult<UserDto>> GetUserByCedula(string cedula)
    {
        try
        {
            var user = await _userService.ObtenerUsuarioPorCedulaAsync(cedula);
            if (user == null)
            {
                return NotFound($"Usuario con cédula {cedula} no encontrado.");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuario con cédula {Cedula}", cedula);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // GET: api/Users/username/{username}
    [HttpGet("username/{username}")]
    public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
    {
        try
        {
            var user = await _userService.ObtenerUsuarioPorNombreUsuarioAsync(username);
            if (user == null)
            {
                return NotFound($"Usuario con nombre de usuario {username} no encontrado.");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuario con nombre de usuario {Username}", username);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // GET: api/Users/rol/{rol}
    [HttpGet("rol/{rol}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(string rol)
    {
        try
        {
            var users = await _userService.ObtenerUsuariosPorRolAsync(rol);
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios con rol {Rol}", rol);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserCreateDto userCreateDto)
    {
        try
        {
            var user = await _userService.CrearUsuarioAsync(userCreateDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { 
                mensaje = "Errores de validación", 
                errores = ex.ValidationErrors 
            });
        }
        catch (DuplicateCedulaException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (DuplicateUsernameException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear usuario");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> PutUser(int id, UserUpdateDto userUpdateDto)
    {
        try
        {
            var user = await _userService.ActualizarUsuarioAsync(id, userUpdateDto);
            if (user == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound($"Usuario con ID {id} no encontrado.");
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { 
                mensaje = "Errores de validación", 
                errores = ex.ValidationErrors 
            });
        }
        catch (DuplicateCedulaException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (DuplicateUsernameException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar usuario con ID {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.EliminarUsuarioAsync(id);
            return NoContent();
        }
        catch (UserNotFoundException)
        {
            return NotFound($"Usuario con ID {id} no encontrado.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar usuario con ID {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // POST: api/Users/auth
    [HttpPost("auth")]
    public async Task<ActionResult<UserLoginResponseDto>> Authenticate(UserLoginDto loginDto)
    {
        try
        {
            var result = await _userService.AutenticarAsync(loginDto);
            if (result.Exito)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al autenticar usuario");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // GET: api/Users/count
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetUserCount()
    {
        try
        {
            var count = await _userService.ContarUsuariosActivosAsync();
            return Ok(count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al contar usuarios");
            return StatusCode(500, "Error interno del servidor");
        }
    }
}