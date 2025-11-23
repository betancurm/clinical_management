using PatientManagementMicroservice.Exceptions;
using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.Validations;

public static class VisitRecordValidator
{
    public static void Validate(VisitRecord visitRecord)
    {
        if (visitRecord.PresionArterialSistolica.HasValue)
        {
            if (visitRecord.PresionArterialSistolica < 80 || visitRecord.PresionArterialSistolica > 250)
                throw new ValidationException("Presión arterial sistólica debe estar entre 80 y 250.");
        }
        if (visitRecord.PresionArterialDiastolica.HasValue)
        {
            if (visitRecord.PresionArterialDiastolica < 50 || visitRecord.PresionArterialDiastolica > 150)
                throw new ValidationException("Presión arterial diastólica debe estar entre 50 y 150.");
        }

        if (visitRecord.Temperatura.HasValue)
        {
            if (visitRecord.Temperatura < 30 || visitRecord.Temperatura > 45)
                throw new ValidationException("Temperatura debe estar entre 30°C y 45°C.");
        }

        if (visitRecord.Pulso.HasValue)
        {
            if (visitRecord.Pulso < 30 || visitRecord.Pulso > 200)
                throw new ValidationException("Pulso debe estar entre 30 y 200 bpm.");
        }

        if (visitRecord.Oxigenacion.HasValue)
        {
            if (visitRecord.Oxigenacion < 50 || visitRecord.Oxigenacion > 100)
                throw new ValidationException("Oxigenación debe estar entre 50% y 100%.");
        }
    }
}