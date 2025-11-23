using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.DTOs;

public class UpdateAppointmentRequest
{
    public DateTime FechaHoraCita { get; set; }
    public string Motivo { get; set; }
    public EstadoCita Estado { get; set; }
}