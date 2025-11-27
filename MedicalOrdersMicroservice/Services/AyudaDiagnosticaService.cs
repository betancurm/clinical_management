using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Exceptions;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Services;

public class AyudaDiagnosticaService : IAyudaDiagnosticaService
{
    private readonly ApplicationDbContext _context;

    public AyudaDiagnosticaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AyudaDiagnosticaDto>> GetAllAyudasDiagnosticasAsync()
    {
        var ayudas = await Task.FromResult(_context.AyudasDiagnosticas.ToList());
        return ayudas.Select(MapToDto);
    }

    public async Task<AyudaDiagnosticaDto> GetAyudaDiagnosticaByIdAsync(Guid id)
    {
        var ayuda = await Task.FromResult(_context.AyudasDiagnosticas.Find(id));
        if (ayuda == null)
            throw new NotFoundException("Ayuda diagnóstica no encontrada.");
        return MapToDto(ayuda);
    }

    public async Task<AyudaDiagnosticaDto> CreateAyudaDiagnosticaAsync(CreateAyudaDiagnosticaDto dto)
    {
        var ayuda = new AyudaDiagnostica
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Tipo = dto.Tipo,
            Costo = dto.Costo,
            RequierePreparacion = dto.RequierePreparacion,
            InstruccionesPreparacion = dto.InstruccionesPreparacion,
            Activo = dto.Activo,
            FechaCreacion = DateTime.Now
        };

        _context.AyudasDiagnosticas.Add(ayuda);
        await _context.SaveChangesAsync();

        return MapToDto(ayuda);
    }

    public async Task<AyudaDiagnosticaDto> UpdateAyudaDiagnosticaAsync(Guid id, UpdateAyudaDiagnosticaDto dto)
    {
        var ayuda = await Task.FromResult(_context.AyudasDiagnosticas.Find(id));
        if (ayuda == null)
            throw new NotFoundException("Ayuda diagnóstica no encontrada.");

        ayuda.Nombre = dto.Nombre;
        ayuda.Descripcion = dto.Descripcion;
        ayuda.Tipo = dto.Tipo;
        ayuda.Costo = dto.Costo;
        ayuda.RequierePreparacion = dto.RequierePreparacion;
        ayuda.InstruccionesPreparacion = dto.InstruccionesPreparacion;
        ayuda.Activo = dto.Activo;

        _context.AyudasDiagnosticas.Update(ayuda);
        await _context.SaveChangesAsync();

        return MapToDto(ayuda);
    }

    public async Task DeleteAyudaDiagnosticaAsync(Guid id)
    {
        var ayuda = await Task.FromResult(_context.AyudasDiagnosticas.Find(id));
        if (ayuda == null)
            throw new NotFoundException("Ayuda diagnóstica no encontrada.");

        _context.AyudasDiagnosticas.Remove(ayuda);
        await _context.SaveChangesAsync();
    }

    private AyudaDiagnosticaDto MapToDto(AyudaDiagnostica ayuda)
    {
        return new AyudaDiagnosticaDto
        {
            Id = ayuda.Id,
            Nombre = ayuda.Nombre,
            Descripcion = ayuda.Descripcion,
            Tipo = ayuda.Tipo,
            Costo = ayuda.Costo,
            RequierePreparacion = ayuda.RequierePreparacion,
            InstruccionesPreparacion = ayuda.InstruccionesPreparacion,
            Activo = ayuda.Activo,
            FechaCreacion = ayuda.FechaCreacion
        };
    }
}