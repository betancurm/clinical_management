using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace MedicalOrdersMicroservice.Services;

public class AppointmentValidationService : IAppointmentValidationService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppointmentValidationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5156"); // Puerto del PatientManagementMicroservice
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string?> GetPatientCedulaByAppointmentIdAsync(Guid appointmentId)
    {
        try
        {
            // Propagar el token JWT del usuario actual
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }

            // Obtener la cita
            var response = await _httpClient.GetAsync($"/api/patients/appointments/{appointmentId}");
            if (response.IsSuccessStatusCode)
            {
                var appointment = await response.Content.ReadFromJsonAsync<AppointmentDto>();
                if (appointment != null && appointment.PatientId != Guid.Empty)
                {
                    // Obtener el paciente usando el PatientId
                    var patientResponse = await _httpClient.GetAsync($"/api/patients/by-id/{appointment.PatientId}");
                    if (patientResponse.IsSuccessStatusCode)
                    {
                        var patient = await patientResponse.Content.ReadFromJsonAsync<PatientDto>();
                        return patient?.NumeroIdentificacion;
                    }
                }
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    private class AppointmentDto
    {
        public Guid PatientId { get; set; }
    }

    private class PatientDto
    {
        public string NumeroIdentificacion { get; set; }
    }
}