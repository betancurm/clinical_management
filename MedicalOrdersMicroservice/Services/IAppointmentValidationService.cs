namespace MedicalOrdersMicroservice.Services;

public interface IAppointmentValidationService
{
    Task<string?> GetPatientCedulaByAppointmentIdAsync(Guid appointmentId);
}