using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Exceptions;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Services;

public class ProcedimientoService : IProcedimientoService
{
    private readonly ApplicationDbContext _context;

    public ProcedimientoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProcedimientoDto>> GetAllProcedimientosAsync()
    {
        var procedimientos = await Task.FromResult(_context.Procedimientos.ToList());
        return procedimientos.Select(MapToDto);
    }

    public async Task<ProcedimientoDto> GetProcedimientoByIdAsync(Guid id)
    {
        var procedimiento = await Task.FromResult(_context.Procedimientos.Find(id));
        if (procedimiento == null)
            throw new NotFoundException("Procedimiento no encontrado.");
        return MapToDto(procedimiento);
    }

    public async Task<ProcedimientoDto> CreateProcedimientoAsync(CreateProcedimientoDto dto)
    {
        var procedimiento = new Procedimiento
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            DuracionEstimadaMinutos = dto.DuracionEstimadaMinutos,
            Costo = dto.Costo,
            Activo = dto.Activo,
            FechaCreacion = DateTime.Now
        };

        _context.Procedimientos.Add(procedimiento);
        await _context.SaveChangesAsync();

        return MapToDto(procedimiento);
    }

    public async Task<ProcedimientoDto> UpdateProcedimientoAsync(Guid id, UpdateProcedimientoDto dto)
    {
        var procedimiento = await Task.FromResult(_context.Procedimientos.Find(id));
        if (procedimiento == null)
            throw new NotFoundException("Procedimiento no encontrado.");

        procedimiento.Nombre = dto.Nombre;
        procedimiento.Descripcion = dto.Descripcion;
        procedimiento.DuracionEstimadaMinutos = dto.DuracionEstimadaMinutos;
        procedimiento.Costo = dto.Costo;
        procedimiento.Activo = dto.Activo;

        _context.Procedimientos.Update(procedimiento);
        await _context.SaveChangesAsync();

        return MapToDto(procedimiento);
    }

    public async Task DeleteProcedimientoAsync(Guid id)
    {
        var procedimiento = await Task.FromResult(_context.Procedimientos.Find(id));
        if (procedimiento == null)
            throw new NotFoundException("Procedimiento no encontrado.");

        _context.Procedimientos.Remove(procedimiento);
        await _context.SaveChangesAsync();
    }

    private ProcedimientoDto MapToDto(Procedimiento procedimiento)
    {
        return new ProcedimientoDto
        {
            Id = procedimiento.Id,
            Nombre = procedimiento.Nombre,
            Descripcion = procedimiento.Descripcion,
            DuracionEstimadaMinutos = procedimiento.DuracionEstimadaMinutos,
            Costo = procedimiento.Costo,
            Activo = procedimiento.Activo,
            FechaCreacion = procedimiento.FechaCreacion
        };
    }
}