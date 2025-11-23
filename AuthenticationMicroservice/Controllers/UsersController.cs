using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Services;

namespace AuthenticationMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "RecursosHumanos")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var user = new User
        {
            PrimerNombre = request.PrimerNombre,
            SegundoNombre = request.SegundoNombre,
            PrimerApellido = request.PrimerApellido,
            SegundoApellido = request.SegundoApellido,
            NumeroCedula = request.NumeroCedula,
            TipoDocumento = request.TipoDocumento,
            CorreoElectronico = request.CorreoElectronico,
            NumeroTelefono = request.NumeroTelefono,
            FechaNacimiento = request.FechaNacimiento,
            Direccion = request.Direccion,
            Rol = request.Rol
        };

        var result = _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = result.User.Id }, result);
    }

    [HttpDelete("{id}")]
    
    public IActionResult DeleteUser(Guid id)
    {
        _userService.DeleteUser(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _userService.GetUserById(id);
        return Ok(user);
    }
}