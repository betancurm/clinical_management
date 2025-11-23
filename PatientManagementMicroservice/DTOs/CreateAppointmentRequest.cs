using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.DTOs;

public class CreateAppointmentRequest
{
    public DateTime FechaHoraCita { get; set; }
    public string Motivo { get; set; }
    public EstadoCita? Estado { get; set; } // Opcional, por defecto Pendiente
}