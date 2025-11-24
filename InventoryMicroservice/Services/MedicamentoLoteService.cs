using Microsoft.EntityFrameworkCore;
using InventoryMicroservice.DTOs;
using InventoryMicroservice.Exceptions;
using InventoryMicroservice.Models;
using InventoryMicroservice.Validations;

namespace InventoryMicroservice.Services;

public class MedicamentoLoteService : IMedicamentoLoteService
{
    private readonly ApplicationDbContext _context;

    public MedicamentoLoteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoteDto> CreateAsync(Guid medicamentoId, CreateLoteDto dto)
    {
        var medicamento = await _context.Medicamentos.FindAsync(medicamentoId);
        if (medicamento == null || !medicamento.Activo)
            throw new NotFoundException("Medicamento no encontrado o inactivo.");

        var lote = new MedicamentoLote
        {
            Id = Guid.NewGuid(),
            MedicamentoId = medicamentoId,
            Lote = dto.Lote,
            FechaFabricacion = dto.FechaFabricacion,
            FechaExpiracion = dto.FechaExpiracion,
            CantidadDisponible = dto.CantidadDisponible,
            Activo = true
        };

        MedicamentoLoteValidator.Validate(lote);

        _context.MedicamentoLotes.Add(lote);
        await _context.SaveChangesAsync();

        return MapToDto(lote);
    }

    public async Task<IEnumerable<LoteDto>> GetByMedicamentoAsync(Guid medicamentoId)
    {
        var medicamento = await _context.Medicamentos.FindAsync(medicamentoId);
        if (medicamento == null)
            throw new NotFoundException("Medicamento no encontrado.");

        var lotes = await _context.MedicamentoLotes
            .Where(l => l.MedicamentoId == medicamentoId)
            .ToListAsync();

        return lotes.Select(MapToDto);
    }

    public async Task<LoteDto> GetByIdAsync(Guid medicamentoId, Guid loteId)
    {
        var lote = await _context.MedicamentoLotes.FindAsync(loteId);
        if (lote == null || lote.MedicamentoId != medicamentoId)
            throw new NotFoundException("Lote no encontrado para este medicamento.");

        return MapToDto(lote);
    }

    public async Task<LoteDto> UpdateAsync(Guid medicamentoId, Guid loteId, UpdateLoteDto dto)
    {
        var lote = await _context.MedicamentoLotes.FindAsync(loteId);
        if (lote == null || lote.MedicamentoId != medicamentoId)
            throw new NotFoundException("Lote no encontrado para este medicamento.");

        lote.Lote = dto.Lote;
        lote.FechaFabricacion = dto.FechaFabricacion;
        lote.FechaExpiracion = dto.FechaExpiracion;
        lote.CantidadDisponible = dto.CantidadDisponible;

        MedicamentoLoteValidator.Validate(lote);

        await _context.SaveChangesAsync();

        return MapToDto(lote);
    }

    public async Task<LoteDto> UpdateCantidadAsync(Guid medicamentoId, Guid loteId, ActualizarCantidadLoteDto dto)
    {
        var lote = await _context.MedicamentoLotes.FindAsync(loteId);
        if (lote == null || lote.MedicamentoId != medicamentoId)
            throw new NotFoundException("Lote no encontrado para este medicamento.");

        if (dto.NuevaCantidad < 0)
            throw new ValidationException("La cantidad no puede ser negativa.");

        lote.CantidadDisponible = dto.NuevaCantidad;

        await _context.SaveChangesAsync();

        return MapToDto(lote);
    }

    public async Task DeleteAsync(Guid medicamentoId, Guid loteId)
    {
        var lote = await _context.MedicamentoLotes.FindAsync(loteId);
        if (lote == null || lote.MedicamentoId != medicamentoId)
            throw new NotFoundException("Lote no encontrado para este medicamento.");

        lote.Activo = false;
        await _context.SaveChangesAsync();
    }

    private LoteDto MapToDto(MedicamentoLote lote)
    {
        return new LoteDto
        {
            Id = lote.Id,
            MedicamentoId = lote.MedicamentoId,
            Lote = lote.Lote,
            FechaFabricacion = lote.FechaFabricacion,
            FechaExpiracion = lote.FechaExpiracion,
            CantidadDisponible = lote.CantidadDisponible,
            Activo = lote.Activo,
            Expirado = lote.FechaExpiracion <= DateTime.UtcNow
        };
    }
}