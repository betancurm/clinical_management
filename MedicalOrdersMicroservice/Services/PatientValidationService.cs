using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace MedicalOrdersMicroservice.Services;

public class PatientValidationService : IPatientValidationService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PatientValidationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        // Configurar base address, asumir que está en appsettings o hardcodeado para ejemplo
        _httpClient.BaseAddress = new Uri("http://localhost:5156"); // Asumir puerto del microservicio de Pacientes
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> PatientExistsAsync(string cedulaPaciente)
    {
        try
        {
            // Propagar el token JWT del usuario actual
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }

            // Endpoint GET /api/patients/{numeroIdentificacion}
            var response = await _httpClient.GetAsync($"/api/patients/{cedulaPaciente}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            // En caso de error de comunicación, asumir no existe para no bloquear
            return false;
        }
    }
}