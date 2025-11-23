using Microsoft.EntityFrameworkCore;
using PatientManagementMicroservice.Exceptions;
using PatientManagementMicroservice.Models;
using PatientManagementMicroservice.Validations;
using Microsoft.EntityFrameworkCore;

namespace PatientManagementMicroservice.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Patient CreatePatient(Patient patient, PatientExtraInfo? extraInfo = null)
    {
        PatientValidator.ValidatePatient(patient);

        // Verificar unicidad NumeroIdentificacion
        if (_context.Patients.Any(p => p.NumeroIdentificacion == patient.NumeroIdentificacion))
            throw new ConflictException("El NumeroIdentificacion ya existe.");

        patient.Id = Guid.NewGuid();

        if (extraInfo != null)
        {
            PatientValidator.ValidatePatientExtraInfo(extraInfo);
            extraInfo.PatientId = patient.Id;
            patient.ExtraInfo = extraInfo;
        }

        _context.Patients.Add(patient);
        _context.SaveChanges();

        return patient;
    }

    public Patient UpdatePatient(string numeroIdentificacion, Patient updatedPatient, PatientExtraInfo? extraInfo = null)
    {
        // Load ExtraInfo to avoid inserting a duplicate extra info record for the same patient
        var existingPatient = _context.Patients
            .Include(p => p.ExtraInfo)
            .FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (existingPatient == null)
            throw new NotFoundException("Paciente no encontrado.");

        PatientValidator.ValidatePatient(updatedPatient);

        // Verificar unicidad NumeroIdentificacion si cambió
        if (updatedPatient.NumeroIdentificacion != existingPatient.NumeroIdentificacion &&
            _context.Patients.Any(p => p.NumeroIdentificacion == updatedPatient.NumeroIdentificacion && p.Id != existingPatient.Id))
            throw new ConflictException("El NumeroIdentificacion ya existe.");

        existingPatient.NumeroIdentificacion = updatedPatient.NumeroIdentificacion;
        existingPatient.NombreCompleto = updatedPatient.NombreCompleto;
        existingPatient.FechaNacimiento = updatedPatient.FechaNacimiento;
        existingPatient.Genero = updatedPatient.Genero;
        existingPatient.Direccion = updatedPatient.Direccion;
        existingPatient.NumeroTelefono = updatedPatient.NumeroTelefono;
        existingPatient.CorreoElectronico = updatedPatient.CorreoElectronico;

        if (extraInfo != null)
        {
            PatientValidator.ValidatePatientExtraInfo(extraInfo);
            if (existingPatient.ExtraInfo == null)
            {
                extraInfo.PatientId = existingPatient.Id;
                _context.PatientExtraInfos.Add(extraInfo);
                existingPatient.ExtraInfo = extraInfo;
            }
            else
            {
                existingPatient.ExtraInfo.PrimerNombreContacto = extraInfo.PrimerNombreContacto;
                existingPatient.ExtraInfo.SegundoNombreContacto = extraInfo.SegundoNombreContacto;
                existingPatient.ExtraInfo.PrimerApellidoContacto = extraInfo.PrimerApellidoContacto;
                existingPatient.ExtraInfo.SegundoApellidoContacto = extraInfo.SegundoApellidoContacto;
                existingPatient.ExtraInfo.RelacionConPaciente = extraInfo.RelacionConPaciente;
                existingPatient.ExtraInfo.NumeroTelefonoEmergencia = extraInfo.NumeroTelefonoEmergencia;
                existingPatient.ExtraInfo.NombreCompaniaSeguros = extraInfo.NombreCompaniaSeguros;
                existingPatient.ExtraInfo.NumeroPoliza = extraInfo.NumeroPoliza;
                existingPatient.ExtraInfo.PolizaActiva = extraInfo.PolizaActiva;
                existingPatient.ExtraInfo.VigenciaPoliza = extraInfo.VigenciaPoliza;
            }
        }

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("duplicate") ?? false || ex.Message.Contains("duplicate"))
            {
                throw new ConflictException("El NumeroIdentificacion ya existe.");
            }
            throw;
        }
        return existingPatient;
    }

    public void DeletePatient(string numeroIdentificacion)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);
        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        _context.Patients.Remove(patient);
        _context.SaveChanges();
    }

    public Patient GetPatientByNumeroIdentificacion(string numeroIdentificacion)
    {
        var patient = _context.Patients
            .Include(p => p.ExtraInfo)
            .Include(p => p.Appointments)
            .FirstOrDefault(p => p.NumeroIdentificacion == numeroIdentificacion);

        if (patient == null)
            throw new NotFoundException("Paciente no encontrado.");

        return patient;
    }

    public IEnumerable<Patient> GetPatients()
    {
        return _context.Patients
            .Include(p => p.ExtraInfo)
            .Include(p => p.Appointments)
            .ToList();
    }
}
