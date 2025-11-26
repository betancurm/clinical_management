using Microsoft.EntityFrameworkCore;
using MedicalOrdersMicroservice.DTOs;
using MedicalOrdersMicroservice.Exceptions;
using MedicalOrdersMicroservice.Models;
using MedicalOrdersMicroservice.Validations;

namespace MedicalOrdersMicroservice.Services;

public class OrdenMedicaService : IOrdenMedicaService
{
    private readonly ApplicationDbContext _context;
    private readonly IPatientValidationService _patientValidationService;
    private readonly IAppointmentValidationService _appointmentValidationService;

    public OrdenMedicaService(ApplicationDbContext context, IPatientValidationService patientValidationService, IAppointmentValidationService appointmentValidationService)
    {
        _context = context;
        _patientValidationService = patientValidationService;
        _appointmentValidationService = appointmentValidationService;
    }

    public async Task<OrdenMedicaDto> CreateAsync(OrdenMedica orden)
    {
        // Obtener cédula del paciente desde la cita
        var cedulaPaciente = await _appointmentValidationService.GetPatientCedulaByAppointmentIdAsync(orden.CitaId);
        if (string.IsNullOrEmpty(cedulaPaciente))
            throw new ValidationException("No se pudo obtener la cédula del paciente desde la cita especificada.");

        orden.CedulaPaciente = cedulaPaciente;

        // Generar NumeroOrden único automáticamente
        string numeroOrden = await GenerateUniqueNumeroOrden();
        orden.NumeroOrden = numeroOrden;

        // Validar que el paciente existe en el microservicio de Pacientes
        if (!await _patientValidationService.PatientExistsAsync(orden.CedulaPaciente))
            throw new ValidationException("El paciente con la cédula especificada no existe.");

        OrdenMedicaValidator.Validate(orden);
        OrdenMedicaValidator.ValidateBusinessRules(orden);

        _context.OrdenesMedicas.Add(orden);
        await _context.SaveChangesAsync();

        return await GetByNumeroOrdenAsync(orden.NumeroOrden);
    }

    public async Task<OrdenMedicaDto> GetByNumeroOrdenAsync(string numeroOrden)
    {
        var orden = await _context.OrdenesMedicas
            .Include(o => o.Medicamentos)
            .Include(o => o.Procedimientos)
            .Include(o => o.AyudasDiagnosticas)
            .FirstOrDefaultAsync(o => o.NumeroOrden == numeroOrden);

        if (orden == null)
            throw new NotFoundException("Orden médica no encontrada.");

        return MapToDto(orden);
    }

    public async Task<IEnumerable<OrdenMedicaDto>> GetByPacienteAsync(string cedulaPaciente)
    {
        var ordenes = await _context.OrdenesMedicas
            .Where(o => o.CedulaPaciente == cedulaPaciente)
            .Include(o => o.Medicamentos)
            .Include(o => o.Procedimientos)
            .Include(o => o.AyudasDiagnosticas)
            .ToListAsync();

        return ordenes.Select(MapToDto);
    }

    public async Task<IEnumerable<OrdenMedicaDto>> GetByMedicoAsync(string cedulaMedico)
    {
        var ordenes = await _context.OrdenesMedicas
            .Where(o => o.CedulaMedico == cedulaMedico)
            .Include(o => o.Medicamentos)
            .Include(o => o.Procedimientos)
            .Include(o => o.AyudasDiagnosticas)
            .ToListAsync();

        return ordenes.Select(MapToDto);
    }

    public async Task<IEnumerable<OrdenMedicaDto>> GetByCitaAsync(Guid citaId)
    {
        var ordenes = await _context.OrdenesMedicas
            .Where(o => o.CitaId == citaId)
            .Include(o => o.Medicamentos)
            .Include(o => o.Procedimientos)
            .Include(o => o.AyudasDiagnosticas)
            .ToListAsync();

        return ordenes.Select(MapToDto);
    }

    public async Task<IEnumerable<OrdenMedicaDto>> GetBatchAsync(IEnumerable<string> numeroOrdenes)
    {
        var ordenes = await _context.OrdenesMedicas
            .Where(o => numeroOrdenes.Contains(o.NumeroOrden))
            .Include(o => o.Medicamentos)
            .Include(o => o.Procedimientos)
            .Include(o => o.AyudasDiagnosticas)
            .ToListAsync();

        return ordenes.Select(MapToDto);
    }

    private async Task<string> GenerateUniqueNumeroOrden()
    {
        Random random = new();
        string numeroOrden;
        do
        {
            numeroOrden = random.Next(100000, 999999).ToString(); // 6 dígitos
        } while (await _context.OrdenesMedicas.AnyAsync(o => o.NumeroOrden == numeroOrden));
        return numeroOrden;
    }

    private OrdenMedicaDto MapToDto(OrdenMedica orden)
    {
        return new OrdenMedicaDto
        {
            NumeroOrden = orden.NumeroOrden,
            CedulaPaciente = orden.CedulaPaciente,
            CedulaMedico = orden.CedulaMedico,
            CitaId = orden.CitaId,
            FechaCreacion = orden.FechaCreacion,
            TipoOrden = orden.TipoOrden,
            Medicamentos = orden.Medicamentos.Select(m => new MedicamentoDetalleDto
            {
                NumeroItem = m.NumeroItem,
                NombreMedicamento = m.NombreMedicamento,
                IdMedicamento = m.IdMedicamento,
                Dosis = m.Dosis,
                DuracionTratamiento = m.DuracionTratamiento,
                Costo = m.Costo
            }),
            Procedimientos = orden.Procedimientos.Select(p => new ProcedimientoDetalleDto
            {
                NumeroItem = p.NumeroItem,
                NombreProcedimiento = p.NombreProcedimiento,
                IdProcedimiento = p.IdProcedimiento,
                NumeroVeces = p.NumeroVeces,
                Frecuencia = p.Frecuencia,
                Costo = p.Costo,
                RequiereEspecialista = p.RequiereEspecialista,
                IdTipoEspecialidad = p.IdTipoEspecialidad
            }),
            AyudasDiagnosticas = orden.AyudasDiagnosticas.Select(a => new AyudaDiagnosticaDetalleDto
            {
                NumeroItem = a.NumeroItem,
                NombreAyudaDiagnostica = a.NombreAyudaDiagnostica,
                IdAyudaDiagnostica = a.IdAyudaDiagnostica,
                Cantidad = a.Cantidad,
                Costo = a.Costo,
                RequiereEspecialista = a.RequiereEspecialista,
                IdTipoEspecialidad = a.IdTipoEspecialidad
            })
        };
    }
}