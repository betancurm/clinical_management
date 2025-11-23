using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagementMicroservice.DTOs;
using PatientManagementMicroservice.Models;
using PatientManagementMicroservice.Services;

namespace PatientManagementMicroservice.Controllers;

[ApiController]
[Route("api/patients/{numeroIdentificacion}/appointments")]
[Authorize(Roles = "PersonalAdministrativo")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public IActionResult CreateAppointment(string numeroIdentificacion, [FromBody] CreateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            FechaHoraCita = request.FechaHoraCita,
            Motivo = request.Motivo,
            Estado = request.Estado ?? EstadoCita.Programada
        };

        var createdAppointment = _appointmentService.CreateAppointment(numeroIdentificacion, appointment);
        return CreatedAtAction(nameof(GetAppointment), new { numeroIdentificacion, appointmentId = createdAppointment.Id }, createdAppointment);
    }

    [HttpGet]
    public IActionResult GetAppointments(string numeroIdentificacion)
    {
        var appointments = _appointmentService.GetAppointmentsByPatientId(numeroIdentificacion);
        return Ok(appointments);
    }

    [HttpGet("{appointmentId}")]
    public IActionResult GetAppointment(string numeroIdentificacion, Guid appointmentId)
    {
        var appointment = _appointmentService.GetAppointmentById(numeroIdentificacion, appointmentId);
        return Ok(appointment);
    }

    [HttpPut("{appointmentId}")]
    public IActionResult UpdateAppointment(string numeroIdentificacion, Guid appointmentId, [FromBody] UpdateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            FechaHoraCita = request.FechaHoraCita,
            Motivo = request.Motivo,
            Estado = request.Estado
        };

        var updatedAppointment = _appointmentService.UpdateAppointment(numeroIdentificacion, appointmentId, appointment);
        return Ok(updatedAppointment);
    }

    [HttpDelete("{appointmentId}")]
    public IActionResult DeleteAppointment(string numeroIdentificacion, Guid appointmentId)
    {
        _appointmentService.DeleteAppointment(numeroIdentificacion, appointmentId);
        return NoContent();
    }
}