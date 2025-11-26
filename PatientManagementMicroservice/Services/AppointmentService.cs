using PatientManagementMicroservice.Exceptions;
using PatientManagementMicroservice.Models;
using PatientManagementMicroservice.Validations;

namespace PatientManagementMicroservice.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Appointment CreateAppointment(string numeroIdentificacion, Appointment appointment)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        PatientValidator.ValidateAppointment(appointment);

        appointment.Id = Guid.NewGuid();
        appointment.PatientId = patient.Id;
        appointment.Estado = appointment.Estado == EstadoCita.Programada ? appointment.Estado : EstadoCita.Programada; // Default to Programada if not set

        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        return appointment;
    }

    public Appointment UpdateAppointment(string numeroIdentificacion, Guid appointmentId, Appointment updatedAppointment)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null || appointment.PatientId != patient.Id)
            throw new NotFoundException("Cita no encontrada para este paciente.");

        PatientValidator.ValidateAppointment(updatedAppointment);

        appointment.FechaHoraCita = updatedAppointment.FechaHoraCita;
        appointment.Motivo = updatedAppointment.Motivo;
        appointment.Estado = updatedAppointment.Estado;

        _context.SaveChanges();
        return appointment;
    }

    public void DeleteAppointment(string numeroIdentificacion, Guid appointmentId)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null || appointment.PatientId != patient.Id)
            throw new NotFoundException("Cita no encontrada para este paciente.");

        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
    }

    public Appointment GetAppointmentById(string numeroIdentificacion, Guid appointmentId)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null || appointment.PatientId != patient.Id)
            throw new NotFoundException("Cita no encontrada para este paciente.");

        return appointment;
    }

    public IEnumerable<Appointment> GetAppointmentsByPatientId(string numeroIdentificacion)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        return _context.Appointments.Where(a => a.PatientId == patient.Id).ToList();
    }

    public Appointment GetAppointmentById(Guid appointmentId)
    {
        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null)
            throw new NotFoundException("Cita no encontrada.");

        return appointment;
    }

    public Appointment UpdateAppointment(Guid appointmentId, Appointment updatedAppointment)
    {
        var appointment = _context.Appointments.Find(appointmentId);
        if (appointment == null)
            throw new NotFoundException("Cita no encontrada.");

        PatientValidator.ValidateAppointment(updatedAppointment);

        appointment.FechaHoraCita = updatedAppointment.FechaHoraCita;
        appointment.Motivo = updatedAppointment.Motivo;
        appointment.Estado = updatedAppointment.Estado;

        _context.SaveChanges();
        return appointment;
    }
}