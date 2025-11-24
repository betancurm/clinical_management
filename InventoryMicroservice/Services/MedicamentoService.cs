using Microsoft.EntityFrameworkCore;
using InventoryMicroservice.DTOs;
using InventoryMicroservice.Exceptions;
using InventoryMicroservice.Models;
using InventoryMicroservice.Validations;

namespace InventoryMicroservice.Services;

public class MedicamentoService : IMedicamentoService
{
    private readonly ApplicationDbContext _context;

    public MedicamentoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MedicamentoDto> CreateAsync(CreateMedicamentoDto dto)
    {
        var medicamento = new Medicamento
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Concentracion = dto.Concentracion,
            FormaFarmaceutica = dto.FormaFarmaceutica,
            Proveedor = dto.Proveedor,
            Activo = true
        };

        MedicamentoValidator.Validate(medicamento);

        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();

        return MapToDto(medicamento, 0);
    }

 public async Task<IEnumerable<MedicamentoDto>> GetAllAsync(MedicamentoFilter filter)
{
    var query = _context.Medicamentos.AsQueryable();

    // Búsqueda parcial sin importar mayúsculas/minúsculas
    if (!string.IsNullOrWhiteSpace(filter.Nombre))
    {
        var termino = filter.Nombre.Trim().ToLower();

        query = query
            .Where(m => m.Nombre.ToLower().Contains(termino))
            .OrderBy(m => m.Nombre.ToLower().IndexOf(termino));
    }

    if (!string.IsNullOrWhiteSpace(filter.Proveedor))
        query = query.Where(m => m.Proveedor.ToLower().Contains(filter.Proveedor.ToLower()));

    if (!string.IsNullOrWhiteSpace(filter.Concentracion))
        query = query.Where(m => m.Concentracion.ToLower().Contains(filter.Concentracion.ToLower()));

    if (filter.FormaFarmaceutica.HasValue)
        query = query.Where(m => m.FormaFarmaceutica == filter.FormaFarmaceutica);

    if (filter.Activo.HasValue)
        query = query.Where(m => m.Activo == filter.Activo);

    var medicamentos = await query.ToListAsync();

    return medicamentos.Select(m => MapToDto(m, CalculateStockTotal(m.Id)));
}



    public async Task<MedicamentoDetalleDto> GetByIdAsync(Guid id)
    {
        var medicamento = await _context.Medicamentos
            .Include(m => m.Lotes)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (medicamento == null)
            throw new NotFoundException("Medicamento no encontrado.");

        var lotes = await _context.MedicamentoLotes
            .Where(l => l.MedicamentoId == id)
            .ToListAsync();

        var dto = new MedicamentoDetalleDto
        {
            Id = medicamento.Id,
            Nombre = medicamento.Nombre,
            Descripcion = medicamento.Descripcion,
            Concentracion = medicamento.Concentracion,
            FormaFarmaceutica = medicamento.FormaFarmaceutica,
            Proveedor = medicamento.Proveedor,
            Activo = medicamento.Activo,
            StockTotal = CalculateStockTotal(id),
            Lotes = lotes.Select(MapToLoteDto)
        };

        return dto;
    }

    public async Task<MedicamentoDto> UpdateAsync(Guid id, UpdateMedicamentoDto dto)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);
        if (medicamento == null)
            throw new NotFoundException("Medicamento no encontrado.");

        medicamento.Nombre = dto.Nombre;
        medicamento.Descripcion = dto.Descripcion;
        medicamento.Concentracion = dto.Concentracion;
        medicamento.FormaFarmaceutica = dto.FormaFarmaceutica;
        medicamento.Proveedor = dto.Proveedor;

        MedicamentoValidator.Validate(medicamento);

        await _context.SaveChangesAsync();

        return MapToDto(medicamento, CalculateStockTotal(id));
    }

    public async Task DeleteAsync(Guid id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);
        if (medicamento == null)
            throw new NotFoundException("Medicamento no encontrado.");

        medicamento.Activo = false;
        await _context.SaveChangesAsync();
    }

    private int CalculateStockTotal(Guid medicamentoId)
    {
        return _context.MedicamentoLotes
            .Where(l => l.MedicamentoId == medicamentoId && l.Activo && l.FechaExpiracion > DateTime.UtcNow)
            .Sum(l => l.CantidadDisponible);
    }

    private MedicamentoDto MapToDto(Medicamento medicamento, int stockTotal)
    {
        return new MedicamentoDto
        {
            Id = medicamento.Id,
            Nombre = medicamento.Nombre,
            Descripcion = medicamento.Descripcion,
            Concentracion = medicamento.Concentracion,
            FormaFarmaceutica = medicamento.FormaFarmaceutica,
            Proveedor = medicamento.Proveedor,
            Activo = medicamento.Activo,
            StockTotal = stockTotal
        };
    }

    private LoteDto MapToLoteDto(MedicamentoLote lote)
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