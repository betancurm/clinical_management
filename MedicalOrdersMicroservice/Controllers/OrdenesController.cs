using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Models;
using MedicalOrdersMicroservice.Services;

namespace MedicalOrdersMicroservice.Controllers;

[ApiController]
[Route("api/ordenes")]
[Authorize(Roles = "Medico,Enfermero,PersonalAdministrativo")]
public class OrdenesController : ControllerBase
{
    private readonly IOrdenMedicaService _ordenService;

    public OrdenesController(IOrdenMedicaService ordenService)
    {
        _ordenService = ordenService;
    }

    [HttpPost]
    [Authorize(Roles = "Medico")]
    public async Task<IActionResult> CreateOrden([FromBody] CreateOrdenMedicaDto dto)
    {
        // Obtener cédula del médico del JWT token
        var cedulaMedico = User.FindFirst("cedula")?.Value;
        if (string.IsNullOrEmpty(cedulaMedico))
        {
            return Unauthorized("No se pudo obtener la cédula del médico del token.");
        }

        var orden = new OrdenMedica
        {
            CedulaMedico = cedulaMedico,
            CitaId = dto.CitaId,
            TipoOrden = dto.TipoOrden
        };

        // Agregar detalles
        int itemCounter = 1;

        foreach (var med in dto.Medicamentos)
        {
            med.NumeroItem = itemCounter++;
            orden.Medicamentos.Add(new OrdenMedicamentoDetalle
            {
                NumeroOrden = orden.NumeroOrden,
                NumeroItem = med.NumeroItem,
                NombreMedicamento = med.NombreMedicamento,
                IdMedicamento = med.IdMedicamento,
                Dosis = med.Dosis,
                DuracionTratamiento = med.DuracionTratamiento,
                Costo = med.Costo
            });
        }

        foreach (var proc in dto.Procedimientos)
        {
            proc.NumeroItem = itemCounter++;
            orden.Procedimientos.Add(new OrdenProcedimientoDetalle
            {
                NumeroOrden = orden.NumeroOrden,
                NumeroItem = proc.NumeroItem,
                NombreProcedimiento = proc.NombreProcedimiento,
                IdProcedimiento = proc.IdProcedimiento,
                NumeroVeces = proc.NumeroVeces,
                Frecuencia = proc.Frecuencia,
                Costo = proc.Costo,
                RequiereEspecialista = proc.RequiereEspecialista,
                IdTipoEspecialidad = proc.RequiereEspecialista ? proc.IdTipoEspecialidad : null
            });
        }

        foreach (var ayuda in dto.AyudasDiagnosticas)
        {
            ayuda.NumeroItem = itemCounter++;
            orden.AyudasDiagnosticas.Add(new OrdenAyudaDiagnosticaDetalle
            {
                NumeroOrden = orden.NumeroOrden,
                NumeroItem = ayuda.NumeroItem,
                NombreAyudaDiagnostica = ayuda.NombreAyudaDiagnostica,
                IdAyudaDiagnostica = ayuda.IdAyudaDiagnostica,
                Cantidad = ayuda.Cantidad,
                Costo = ayuda.Costo,
                RequiereEspecialista = ayuda.RequiereEspecialista,
                IdTipoEspecialidad = ayuda.RequiereEspecialista ? ayuda.IdTipoEspecialidad : null
            });
        }

        var result = await _ordenService.CreateAsync(orden);
        return CreatedAtAction(nameof(GetOrden), new { numeroOrden = result.NumeroOrden }, result);
    }

    [HttpGet("{numeroOrden}")]
    public async Task<IActionResult> GetOrden(string numeroOrden)
    {
        var orden = await _ordenService.GetByNumeroOrdenAsync(numeroOrden);
        return Ok(orden);
    }

    [HttpGet("paciente/{cedulaPaciente}")]
    public async Task<IActionResult> GetOrdenesByPaciente(string cedulaPaciente)
    {
        var ordenes = await _ordenService.GetByPacienteAsync(cedulaPaciente);
        return Ok(ordenes);
    }

    [HttpGet("medico/{cedulaMedico}")]
    public async Task<IActionResult> GetOrdenesByMedico(string cedulaMedico)
    {
        var ordenes = await _ordenService.GetByMedicoAsync(cedulaMedico);
        return Ok(ordenes);
    }

    [HttpGet("cita/{citaId}")]
    public async Task<IActionResult> GetOrdenesByCita(Guid citaId)
    {
        var ordenes = await _ordenService.GetByCitaAsync(citaId);
        return Ok(ordenes);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> GetBatchOrdenes([FromBody] BatchOrdenesDto dto)
    {
        var ordenes = await _ordenService.GetBatchAsync(dto.NumeroOrdenes);
        return Ok(ordenes);
    }
}