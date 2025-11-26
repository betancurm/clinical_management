using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryMicroservice.DTOs;
using InventoryMicroservice.Services;

namespace InventoryMicroservice.Controllers;

[ApiController]
[Route("api/medicamentos")]
[Authorize(Roles = "SoporteTecnico,Enfermero,Medico")]
public class MedicamentosController : ControllerBase
{
    private readonly IMedicamentoService _medicamentoService;

    public MedicamentosController(IMedicamentoService medicamentoService)
    {
        _medicamentoService = medicamentoService;
    }

    [HttpPost]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> CreateMedicamento([FromBody] CreateMedicamentoDto dto)
    {
        var medicamento = await _medicamentoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.Id }, medicamento);
    }

    [HttpGet]
    //[Authorize(Roles = "SoporteTecnico,Enfermero,Medico")]

    public async Task<IActionResult> GetMedicamentos([FromQuery] MedicamentoFilter filter)
    {
        // If no filter is provided, return all medicamentos

        if (filter == null)
        {
            filter = new MedicamentoFilter();
        }
        var medicamentos = await _medicamentoService.GetAllAsync(filter );
        return Ok(medicamentos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicamento(Guid id)
    {
        var medicamento = await _medicamentoService.GetByIdAsync(id);
        return Ok(medicamento);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> UpdateMedicamento(Guid id, [FromBody] UpdateMedicamentoDto dto)
    {
        var medicamento = await _medicamentoService.UpdateAsync(id, dto);
        return Ok(medicamento);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SoporteTecnico")]
    public async Task<IActionResult> DeleteMedicamento(Guid id)
    {
        await _medicamentoService.DeleteAsync(id);
        return NoContent();
    }
}