namespace MedicalOrdersMicroservice.Services;

public interface IPatientValidationService
{
    Task<bool> PatientExistsAsync(string cedulaPaciente);
}