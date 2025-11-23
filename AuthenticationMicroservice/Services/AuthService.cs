using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AuthenticationMicroservice.Models;

namespace AuthenticationMicroservice.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    // Clave secreta hardcodeada para ejemplo - mover a configuración segura en producción
    private const string SecretKey = "MiClaveSecretaSuperLargaParaJWTQueDebeSerDeAlMenos32CaracteresParaSeguridad";

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public string? Authenticate(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.NombreUsuario == username);
        if (user == null) return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.ContrasenaHash, password);
        if (result == PasswordVerificationResult.Success)
        {
            return GenerateToken(user);
        }
        return null;
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Rol.ToString()),
            new Claim("username", user.NombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "AuthenticationMicroservice",
            audience: "ClinicalManagement",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}