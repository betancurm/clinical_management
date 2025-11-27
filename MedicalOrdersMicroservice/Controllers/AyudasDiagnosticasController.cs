using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Services;

namespace MedicalOrdersMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SoporteTecnico,Medico")]
public class AyudasDiagnosticasController : ControllerBase
{
    private readonly IAyudaDiagnosticaService _ayudaDiagnosticaService;

    public AyudasDiagnosticasController(IAyudaDiagnosticaService ayudaDiagnosticaService)
    {
        _ayudaDiagnosticaService = ayudaDiagnosticaService;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetAllAyudasDiagnosticas()
    {
        var ayudas = await _ayudaDiagnosticaService.GetAllAyudasDiagnosticasAsync();
        return Ok(ayudas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAyudaDiagnosticaById(Guid id)
    {
        var ayuda = await _ayudaDiagnosticaService.GetAyudaDiagnosticaByIdAsync(id);
        return Ok(ayuda);
    }

    [HttpPost]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> CreateAyudaDiagnostica([FromBody] CreateAyudaDiagnosticaDto dto)
    {
        var ayuda = await _ayudaDiagnosticaService.CreateAyudaDiagnosticaAsync(dto);
        return CreatedAtAction(nameof(GetAyudaDiagnosticaById), new { id = ayuda.Id }, ayuda);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> UpdateAyudaDiagnostica(Guid id, [FromBody] UpdateAyudaDiagnosticaDto dto)
    {
        var ayuda = await _ayudaDiagnosticaService.UpdateAyudaDiagnosticaAsync(id, dto);
        return Ok(ayuda);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> DeleteAyudaDiagnostica(Guid id)
    {
        await _ayudaDiagnosticaService.DeleteAyudaDiagnosticaAsync(id);
        return NoContent();
    }
}