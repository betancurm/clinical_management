using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationMicroservice.Models;
using AuthenticationMicroservice.Services;

namespace AuthenticationMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var token = _authService.Authenticate(request.Username, request.Password);
        if (token == null)
        {
            return Unauthorized(new { Message = "Credenciales inválidas." });
        }
        return Ok(new { Token = token });
    }
}