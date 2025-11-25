using System.Net.Http.Json;

namespace MedicalOrdersMicroservice.Services;

public class PatientValidationService : IPatientValidationService
{
    private readonly HttpClient _httpClient;

    public PatientValidationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Configurar base address, asumir que está en appsettings o hardcodeado para ejemplo
        _httpClient.BaseAddress = new Uri("http://localhost:5156"); // Asumir puerto del microservicio de Pacientes
    }

    public async Task<bool> PatientExistsAsync(string cedulaPaciente)
    {
        try
        {
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