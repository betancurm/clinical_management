using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.Services;

public interface IAppointmentService
{
    Appointment CreateAppointment(string numeroIdentificacion, Appointment appointment);
    Appointment UpdateAppointment(string numeroIdentificacion, Guid appointmentId, Appointment updatedAppointment);
    void DeleteAppointment(string numeroIdentificacion, Guid appointmentId);
    Appointment GetAppointmentById(string numeroIdentificacion, Guid appointmentId);
    IEnumerable<Appointment> GetAppointmentsByPatientId(string numeroIdentificacion);
}