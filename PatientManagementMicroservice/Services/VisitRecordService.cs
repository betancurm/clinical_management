using Microsoft.EntityFrameworkCore;
using PatientManagementMicroservice.DTOs;
using PatientManagementMicroservice.Exceptions;
using PatientManagementMicroservice.Models;
using PatientManagementMicroservice.Validations;

namespace PatientManagementMicroservice.Services;

public class VisitRecordService : IVisitRecordService
{
    private readonly ApplicationDbContext _context;

    public VisitRecordService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VisitRecordDto> CreateAsync(string numeroIdentificacion, CreateVisitRecordDto dto)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var visitRecord = new VisitRecord
        {
            Id = Guid.NewGuid(),
            PatientId = patient.Id,
            FechaVisita = DateTime.UtcNow,
            PresionArterialSistolica = dto.PresionArterialSistolica,
            PresionArterialDiastolica = dto.PresionArterialDiastolica,
            Temperatura = dto.Temperatura,
            Pulso = dto.Pulso,
            Oxigenacion = dto.Oxigenacion,
            Notas = dto.Notas
        };

        VisitRecordValidator.Validate(visitRecord);

        _context.VisitRecords.Add(visitRecord);
        await _context.SaveChangesAsync();

        return MapToDto(visitRecord);
    }

    public async Task<VisitRecordDto> UpdateAsync(string numeroIdentificacion, Guid visitId, UpdateVisitRecordDto dto)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var visitRecord = await _context.VisitRecords.FindAsync(visitId);
        if (visitRecord == null || visitRecord.PatientId != patient.Id)
            throw new NotFoundException("Registro de visita no encontrado para este paciente.");

        visitRecord.PresionArterialSistolica = dto.PresionArterialSistolica;
        visitRecord.PresionArterialDiastolica = dto.PresionArterialDiastolica;
        visitRecord.Temperatura = dto.Temperatura;
        visitRecord.Pulso = dto.Pulso;
        visitRecord.Oxigenacion = dto.Oxigenacion;
        visitRecord.Notas = dto.Notas;

        VisitRecordValidator.Validate(visitRecord);

        await _context.SaveChangesAsync();

        return MapToDto(visitRecord);
    }

    public async Task<IEnumerable<VisitRecordDto>> GetByPatientAsync(string numeroIdentificacion)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var visitRecords = await _context.VisitRecords
            .Where(v => v.PatientId == patient.Id)
            .OrderByDescending(v => v.FechaVisita)
            .ToListAsync();

        return visitRecords.Select(MapToDto);
    }

    public async Task<VisitRecordDto> GetByIdAsync(string numeroIdentificacion, Guid visitId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        var visitRecord = await _context.VisitRecords.FindAsync(visitId);
        if (visitRecord == null || visitRecord.PatientId != patient.Id)
            throw new NotFoundException("Registro de visita no encontrado para este paciente.");

        return MapToDto(visitRecord);
    }

    private static VisitRecordDto MapToDto(VisitRecord visitRecord)
    {
        return new VisitRecordDto
        {
            Id = visitRecord.Id,
            PatientId = visitRecord.PatientId,
            FechaVisita = visitRecord.FechaVisita,
            PresionArterialSistolica = visitRecord.PresionArterialSistolica,
            PresionArterialDiastolica = visitRecord.PresionArterialDiastolica,
            Temperatura = visitRecord.Temperatura,
            Pulso = visitRecord.Pulso,
            Oxigenacion = visitRecord.Oxigenacion,
            Notas = visitRecord.Notas
        };
    }
}