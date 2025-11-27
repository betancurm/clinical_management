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
            CorreoElectronico = request.CorreoElectronico,
            NumeroTelefono = request.NumeroTelefono,
            FechaNacimiento = request.FechaNacimiento,
            Direccion = request.Direccion,
            Rol = request.Rol
        };

        var result = _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { numeroCedula = result.User.NumeroCedula }, result);
    }

    [HttpPut("{numeroCedula}")]
    public IActionResult UpdateUser(string numeroCedula, [FromBody] UpdateUserRequest request)
    {
        _userService.UpdateUser(numeroCedula, request);
        return NoContent();
    }

    [HttpDelete("{numeroCedula}")]
    public IActionResult DeleteUser(string numeroCedula)
    {
        _userService.DeleteUserByNumeroCedula(numeroCedula);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{numeroCedula}")]
    public IActionResult GetUser(string numeroCedula)
    {
        var user = _userService.GetUserByNumeroCedula(numeroCedula);
        return Ok(user);
    }
}