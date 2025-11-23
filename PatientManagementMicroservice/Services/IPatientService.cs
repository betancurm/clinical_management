using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.Services;

public interface IPatientService
{
    Patient CreatePatient(Patient patient, PatientExtraInfo? extraInfo = null);
    Patient UpdatePatient(string numeroIdentificacion, Patient updatedPatient, PatientExtraInfo? extraInfo = null);
    void DeletePatient(string numeroIdentificacion);
    Patient GetPatientByNumeroIdentificacion(string numeroIdentificacion);
    IEnumerable<Patient> GetPatients();
}