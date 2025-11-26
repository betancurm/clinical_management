using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryMicroservice.DTOs;
using InventoryMicroservice.Services;

namespace InventoryMicroservice.Controllers;

[ApiController]
[Route("api/medicamentos/{medicamentoId}/lotes")]
[Authorize(Roles = "SoporteTecnico,Enfermero")]
public class MedicamentoLotesController : ControllerBase
{
    private readonly IMedicamentoLoteService _loteService;

    public MedicamentoLotesController(IMedicamentoLoteService loteService)
    {
        _loteService = loteService;
    }

    [HttpPost]
   [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> CreateLote(Guid medicamentoId, [FromBody] CreateLoteDto dto)
    {
        var lote = await _loteService.CreateAsync(medicamentoId, dto);
        return CreatedAtAction(nameof(GetLote), new { medicamentoId, loteId = lote.Id }, lote);
    }

    [HttpGet]
    public async Task<IActionResult> GetLotes(Guid medicamentoId)
    {
        var lotes = await _loteService.GetByMedicamentoAsync(medicamentoId);
        return Ok(lotes);
    }

    [HttpGet("{loteId}")]
    public async Task<IActionResult> GetLote(Guid medicamentoId, Guid loteId)
    {
        var lote = await _loteService.GetByIdAsync(medicamentoId, loteId);
        return Ok(lote);
    }

    [HttpPut("{loteId}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> UpdateLote(Guid medicamentoId, Guid loteId, [FromBody] UpdateLoteDto dto)
    {
        var lote = await _loteService.UpdateAsync(medicamentoId, loteId, dto);
        return Ok(lote);
    }

    [HttpPatch("{loteId}/cantidad")]
    //[Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> UpdateCantidad(Guid medicamentoId, Guid loteId, [FromBody] ActualizarCantidadLoteDto dto)
    {
        var lote = await _loteService.UpdateCantidadAsync(medicamentoId, loteId, dto);
        return Ok(lote);
    }

    [HttpDelete("{loteId}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> DeleteLote(Guid medicamentoId, Guid loteId)
    {
        await _loteService.DeleteAsync(medicamentoId, loteId);
        return NoContent();
    }
}