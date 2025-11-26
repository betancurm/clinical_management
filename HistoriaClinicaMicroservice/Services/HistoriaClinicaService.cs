using HistoriaClinicaMicroservice.Configurations;
using HistoriaClinicaMicroservice.Exceptions;
using HistoriaClinicaMicroservice.Models;
using HistoriaClinicaMicroservice.Validations;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace HistoriaClinicaMicroservice.Services;

public class HistoriaClinicaService : IHistoriaClinicaService
{
    private readonly MongoDbContext _context;

    public HistoriaClinicaService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAtencionAsync(RegistrarAtencionDto dto)
    {
        HistoriaClinicaValidator.Validate(dto);

        // Buscar o crear documento de historia clínica
        var filter = Builders<HistoriaClinicaDocumento>.Filter.Eq(h => h.CedulaPaciente, dto.CedulaPaciente);
        var historia = await _context.HistoriaClinica.Find(filter).FirstOrDefaultAsync();

        if (historia == null)
        {
            historia = new HistoriaClinicaDocumento
            {
                CedulaPaciente = dto.CedulaPaciente,
                Atenciones = new List<AtencionHistoriaClinica>()
            };
        }

        // Validar reglas de negocio
        HistoriaClinicaValidator.ValidateBusinessRules(dto, historia.Atenciones);

        // Crear nueva atención
        var atencion = new AtencionHistoriaClinica
        {
            FechaAtencion = dto.FechaAtencion,
            CedulaMedico = dto.CedulaMedico,
            MotivoConsulta = dto.MotivoConsulta,
            Sintomatologia = dto.Sintomatologia,
            Diagnostico = dto.Diagnostico,
            CitaId = dto.CitaId,
            NumerosOrdenAsociadas = dto.NumerosOrdenAsociadas,
            NotasAdicionales = dto.NotasAdicionales,
            DatosAdicionales = dto.DatosAdicionales != null ? BsonDocument.Parse(JsonSerializer.Serialize(dto.DatosAdicionales)) : null,
            EsPreAyudaDiagnostica = dto.EsPreAyudaDiagnostica
        };

        historia.Atenciones.Add(atencion);

        // Guardar o actualizar
        if (string.IsNullOrEmpty(historia.Id))
        {
            await _context.HistoriaClinica.InsertOneAsync(historia);
        }
        else
        {
            await _context.HistoriaClinica.ReplaceOneAsync(filter, historia);
        }
    }

    public async Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriaPorPacienteAsync(string cedulaPaciente)
    {
        var filter = Builders<HistoriaClinicaDocumento>.Filter.Eq(h => h.CedulaPaciente, cedulaPaciente);
        var historia = await _context.HistoriaClinica.Find(filter).FirstOrDefaultAsync();

        if (historia == null)
            throw new NotFoundException("Historia clínica no encontrada para el paciente.");

        return historia.Atenciones
            .OrderByDescending(a => a.FechaAtencion)
            .Select(MapToDto);
    }

    public async Task<AtencionHistoriaClinicaDto> ObtenerAtencionPorFechaAsync(string cedulaPaciente, DateTime fechaAtencion)
    {
        var filter = Builders<HistoriaClinicaDocumento>.Filter.Eq(h => h.CedulaPaciente, cedulaPaciente);
        var historia = await _context.HistoriaClinica.Find(filter).FirstOrDefaultAsync();

        if (historia == null)
            throw new NotFoundException("Historia clínica no encontrada para el paciente.");

        var atencion = historia.Atenciones.FirstOrDefault(a => a.FechaAtencion == fechaAtencion);
        if (atencion == null)
            throw new NotFoundException("Atención no encontrada para la fecha especificada.");

        return MapToDto(atencion);
    }

    public async Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriaPorRangoFechasAsync(string cedulaPaciente, DateTime fechaInicio, DateTime fechaFin)
    {
        var filter = Builders<HistoriaClinicaDocumento>.Filter.Eq(h => h.CedulaPaciente, cedulaPaciente);
        var historia = await _context.HistoriaClinica.Find(filter).FirstOrDefaultAsync();

        if (historia == null)
            throw new NotFoundException("Historia clínica no encontrada para el paciente.");

        return historia.Atenciones
            .Where(a => a.FechaAtencion >= fechaInicio && a.FechaAtencion <= fechaFin)
            .OrderByDescending(a => a.FechaAtencion)
            .Select(MapToDto);
    }

    public async Task<IEnumerable<AtencionHistoriaClinicaDto>> ObtenerHistoriasPorMedicoAsync(string cedulaMedico)
    {
        var filter = Builders<HistoriaClinicaDocumento>.Filter.ElemMatch(h => h.Atenciones, a => a.CedulaMedico == cedulaMedico);
        var historias = await _context.HistoriaClinica.Find(filter).ToListAsync();

        return historias
            .SelectMany(h => h.Atenciones.Where(a => a.CedulaMedico == cedulaMedico))
            .OrderByDescending(a => a.FechaAtencion)
            .Select(MapToDto);
    }

    private AtencionHistoriaClinicaDto MapToDto(AtencionHistoriaClinica atencion)
    {
        return new AtencionHistoriaClinicaDto
        {
            FechaAtencion = atencion.FechaAtencion,
            CedulaMedico = atencion.CedulaMedico,
            MotivoConsulta = atencion.MotivoConsulta,
            Sintomatologia = atencion.Sintomatologia,
            Diagnostico = atencion.Diagnostico,
            CitaId = atencion.CitaId,
            NumerosOrdenAsociadas = atencion.NumerosOrdenAsociadas,
            NotasAdicionales = atencion.NotasAdicionales,
            DatosAdicionales = atencion.DatosAdicionales != null ? JsonSerializer.Deserialize<Dictionary<string, object>>(atencion.DatosAdicionales.ToJson()) : null,
            EsPreAyudaDiagnostica = atencion.EsPreAyudaDiagnostica
        };
    }
}