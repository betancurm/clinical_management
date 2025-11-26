using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HistoriaClinicaMicroservice.Models;
using HistoriaClinicaMicroservice.Services;

namespace HistoriaClinicaMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HistoriaClinicaController : ControllerBase
{
    private readonly IHistoriaClinicaService _historiaClinicaService;

    public HistoriaClinicaController(IHistoriaClinicaService historiaClinicaService)
    {
        _historiaClinicaService = historiaClinicaService;
    }

    [HttpPost("atenciones")]
    [Authorize(Roles = "Medico")]
    public async Task<IActionResult> RegistrarAtencion([FromBody] RegistrarAtencionDto dto)
    {
        await _historiaClinicaService.RegistrarAtencionAsync(dto);
        return Created("", new { Message = "Atención registrada exitosamente." });
    }

    [HttpGet("paciente/{cedulaPaciente}")]
    [Authorize(Roles = "Medico,Enfermero")]
    public async Task<IActionResult> ObtenerHistoriaPorPaciente(string cedulaPaciente)
    {
        var atenciones = await _historiaClinicaService.ObtenerHistoriaPorPacienteAsync(cedulaPaciente);
        return Ok(atenciones);
    }

    [HttpGet("paciente/{cedulaPaciente}/atencion")]
    [Authorize(Roles = "Medico,Enfermero")]
    public async Task<IActionResult> ObtenerAtencionPorFecha(string cedulaPaciente, [FromQuery] DateTime fechaAtencion)
    {
        var atencion = await _historiaClinicaService.ObtenerAtencionPorFechaAsync(cedulaPaciente, fechaAtencion);
        return Ok(atencion);
    }

    [HttpGet("paciente/{cedulaPaciente}/rango")]
    [Authorize(Roles = "Medico,Enfermero")]
    public async Task<IActionResult> ObtenerHistoriaPorRangoFechas(string cedulaPaciente, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        var atenciones = await _historiaClinicaService.ObtenerHistoriaPorRangoFechasAsync(cedulaPaciente, fechaInicio, fechaFin);
        return Ok(atenciones);
    }

    [HttpGet("medico/{cedulaMedico}")]
    [Authorize(Roles = "Medico,Enfermero")]
    public async Task<IActionResult> ObtenerHistoriasPorMedico(string cedulaMedico)
    {
        var atenciones = await _historiaClinicaService.ObtenerHistoriasPorMedicoAsync(cedulaMedico);
        return Ok(atenciones);
    }
}