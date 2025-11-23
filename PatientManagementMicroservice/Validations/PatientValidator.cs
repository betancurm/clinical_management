using System.Text.RegularExpressions;
using PatientManagementMicroservice.Exceptions;
using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.Validations;

public static class PatientValidator
{
    public static void ValidatePatient(Patient patient)
    {
        if (string.IsNullOrWhiteSpace(patient.NumeroIdentificacion))
            throw new ValidationException("NumeroIdentificacion es requerido.");

        if (!Regex.IsMatch(patient.NumeroIdentificacion, @"^\d+$"))
            throw new ValidationException("NumeroIdentificacion debe contener solo números.");

        if (string.IsNullOrWhiteSpace(patient.NombreCompleto))
            throw new ValidationException("NombreCompleto es requerido.");

        if (patient.FechaNacimiento > DateTime.Now)
            throw new ValidationException("FechaNacimiento no puede ser futura.");

        int age = DateTime.Now.Year - patient.FechaNacimiento.Year;
        if (patient.FechaNacimiento > DateTime.Now.AddYears(-age)) age--;
        if (age > 150)
            throw new ValidationException("Edad máxima permitida es 150 años.");

        if (!Enum.IsDefined(typeof(Genero), patient.Genero))
            throw new ValidationException("Genero no es válido.");

        if (string.IsNullOrWhiteSpace(patient.Direccion))
            throw new ValidationException("Direccion es requerida.");

        if (string.IsNullOrWhiteSpace(patient.NumeroTelefono))
            throw new ValidationException("NumeroTelefono es requerido.");

        if (!Regex.IsMatch(patient.NumeroTelefono, @"^\d{10}$"))
            throw new ValidationException("NumeroTelefono debe contener exactamente 10 dígitos.");

        if (!string.IsNullOrWhiteSpace(patient.CorreoElectronico) &&
            !Regex.IsMatch(patient.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ValidationException("CorreoElectronico no tiene formato válido.");
    }

    public static void ValidatePatientExtraInfo(PatientExtraInfo extraInfo)
    {
        if (string.IsNullOrWhiteSpace(extraInfo.PrimerNombreContacto))
            throw new ValidationException("PrimerNombreContacto es requerido.");

        if (string.IsNullOrWhiteSpace(extraInfo.PrimerApellidoContacto))
            throw new ValidationException("PrimerApellidoContacto es requerido.");

        if (string.IsNullOrWhiteSpace(extraInfo.RelacionConPaciente))
            throw new ValidationException("RelacionConPaciente es requerida.");

        if (string.IsNullOrWhiteSpace(extraInfo.NumeroTelefonoEmergencia))
            throw new ValidationException("NumeroTelefonoEmergencia es requerido.");

        if (!Regex.IsMatch(extraInfo.NumeroTelefonoEmergencia, @"^\d{10}$"))
            throw new ValidationException("NumeroTelefonoEmergencia debe contener exactamente 10 dígitos.");

        if (string.IsNullOrWhiteSpace(extraInfo.NombreCompaniaSeguros))
            throw new ValidationException("NombreCompaniaSeguros es requerido.");

        if (string.IsNullOrWhiteSpace(extraInfo.NumeroPoliza))
            throw new ValidationException("NumeroPoliza es requerido.");

        // Validar PolizaActiva basado en VigenciaPoliza
        if (extraInfo.VigenciaPoliza < DateTime.Now.Date)
        {
            if (extraInfo.PolizaActiva)
                throw new ValidationException("No se puede marcar la póliza como activa si la vigencia ha expirado.");
        }
        else
        {
            // Si no ha expirado, se puede marcar como activa o inactiva
        }
    }

    public static void ValidateAppointment(Appointment appointment)
    {
        if (appointment.FechaHoraCita <= DateTime.Now)
            throw new ValidationException("FechaHoraCita debe ser futura.");

        if (string.IsNullOrWhiteSpace(appointment.Motivo))
            throw new ValidationException("Motivo es requerido.");
    }
}