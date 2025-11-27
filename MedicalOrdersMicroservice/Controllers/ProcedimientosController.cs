using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Services;

namespace MedicalOrdersMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SoporteTecnico,Medico")]
public class ProcedimientosController : ControllerBase
{
    private readonly IProcedimientoService _procedimientoService;

    public ProcedimientosController(IProcedimientoService procedimientoService)
    {
        _procedimientoService = procedimientoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProcedimientos()
    {
        var procedimientos = await _procedimientoService.GetAllProcedimientosAsync();
        return Ok(procedimientos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProcedimientoById(Guid id)
    {
        var procedimiento = await _procedimientoService.GetProcedimientoByIdAsync(id);
        return Ok(procedimiento);
    }

    [HttpPost]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> CreateProcedimiento([FromBody] CreateProcedimientoDto dto)
    {
        var procedimiento = await _procedimientoService.CreateProcedimientoAsync(dto);
        return CreatedAtAction(nameof(GetProcedimientoById), new { id = procedimiento.Id }, procedimiento);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> UpdateProcedimiento(Guid id, [FromBody] UpdateProcedimientoDto dto)
    {
        var procedimiento = await _procedimientoService.UpdateProcedimientoAsync(id, dto);
        return Ok(procedimiento);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> DeleteProcedimiento(Guid id)
    {
        await _procedimientoService.DeleteProcedimientoAsync(id);
        return NoContent();
    }
}