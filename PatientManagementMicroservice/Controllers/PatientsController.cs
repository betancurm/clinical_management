using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagementMicroservice.DTOs;
using PatientManagementMicroservice.Models;
using PatientManagementMicroservice.Services;

namespace PatientManagementMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Medico,PersonalAdministrativo")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public IActionResult CreatePatient([FromBody] CreatePatientRequest request)
    {
        var patient = new Patient
        {
            NumeroIdentificacion = request.Patient.NumeroIdentificacion,
            NombreCompleto = request.Patient.NombreCompleto,
            FechaNacimiento = request.Patient.FechaNacimiento,
            Genero = request.Patient.Genero,
            Direccion = request.Patient.Direccion,
            NumeroTelefono = request.Patient.NumeroTelefono,
            CorreoElectronico = request.Patient.CorreoElectronico
        };

        PatientExtraInfo? extraInfo = null;
        if (request.ExtraInfo != null)
        {
            extraInfo = new PatientExtraInfo
            {
                PrimerNombreContacto = request.ExtraInfo.PrimerNombreContacto,
                SegundoNombreContacto = request.ExtraInfo.SegundoNombreContacto,
                PrimerApellidoContacto = request.ExtraInfo.PrimerApellidoContacto,
                SegundoApellidoContacto = request.ExtraInfo.SegundoApellidoContacto,
                RelacionConPaciente = request.ExtraInfo.RelacionConPaciente,
                NumeroTelefonoEmergencia = request.ExtraInfo.NumeroTelefonoEmergencia,
                NombreCompaniaSeguros = request.ExtraInfo.NombreCompaniaSeguros,
                NumeroPoliza = request.ExtraInfo.NumeroPoliza,
                PolizaActiva = request.ExtraInfo.PolizaActiva,
                VigenciaPoliza = request.ExtraInfo.VigenciaPoliza
            };
        }

        var createdPatient = _patientService.CreatePatient(patient, extraInfo);
        return CreatedAtAction(nameof(GetPatient), new { numeroIdentificacion = createdPatient.NumeroIdentificacion }, createdPatient);
    }

    [HttpGet]
    public IActionResult GetPatients()
    {
        var patients = _patientService.GetPatients();
        return Ok(patients);
    }

    [HttpGet("{numeroIdentificacion}")]
    public IActionResult GetPatient(string numeroIdentificacion)
    {
        var patient = _patientService.GetPatientByNumeroIdentificacion(numeroIdentificacion);
        return Ok(patient);
    }

    [HttpGet("by-id/{id}")]
    public IActionResult GetPatientById(Guid id)
    {
        var patient = _patientService.GetPatientById(id);
        return Ok(patient);
    }

    [HttpPut("{numeroIdentificacion}")]
    public IActionResult UpdatePatient(string numeroIdentificacion, [FromBody] CreatePatientRequest request)
    {
        var patient = new Patient
        {
            NumeroIdentificacion = request.Patient.NumeroIdentificacion,
            NombreCompleto = request.Patient.NombreCompleto,
            FechaNacimiento = request.Patient.FechaNacimiento,
            Genero = request.Patient.Genero,
            Direccion = request.Patient.Direccion,
            NumeroTelefono = request.Patient.NumeroTelefono,
            CorreoElectronico = request.Patient.CorreoElectronico
        };

        PatientExtraInfo? extraInfo = null;
        if (request.ExtraInfo != null)
        {
            extraInfo = new PatientExtraInfo
            {
                PrimerNombreContacto = request.ExtraInfo.PrimerNombreContacto,
                SegundoNombreContacto = request.ExtraInfo.SegundoNombreContacto,
                PrimerApellidoContacto = request.ExtraInfo.PrimerApellidoContacto,
                SegundoApellidoContacto = request.ExtraInfo.SegundoApellidoContacto,
                RelacionConPaciente = request.ExtraInfo.RelacionConPaciente,
                NumeroTelefonoEmergencia = request.ExtraInfo.NumeroTelefonoEmergencia,
                NombreCompaniaSeguros = request.ExtraInfo.NombreCompaniaSeguros,
                NumeroPoliza = request.ExtraInfo.NumeroPoliza,
                PolizaActiva = request.ExtraInfo.PolizaActiva,
                VigenciaPoliza = request.ExtraInfo.VigenciaPoliza
            };
        }

        var updatedPatient = _patientService.UpdatePatient(numeroIdentificacion, patient, extraInfo);
        return Ok(updatedPatient);
    }

    [HttpDelete("{numeroIdentificacion}")]
    public IActionResult DeletePatient(string numeroIdentificacion)
    {
        _patientService.DeletePatient(numeroIdentificacion);
        return NoContent();
    }
}
