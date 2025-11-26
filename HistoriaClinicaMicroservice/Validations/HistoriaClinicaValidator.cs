using HistoriaClinicaMicroservice.Exceptions;
using HistoriaClinicaMicroservice.Models;

namespace HistoriaClinicaMicroservice.Validations;

public static class HistoriaClinicaValidator
{
    public static void Validate(RegistrarAtencionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CedulaPaciente))
            throw new ValidationException("CedulaPaciente es obligatoria.");

        if (string.IsNullOrWhiteSpace(dto.CedulaMedico) || dto.CedulaMedico.Length > 10)
            throw new ValidationException("CedulaMedico debe tener máximo 10 dígitos y no estar vacío.");

        if (dto.CitaId == Guid.Empty)
            throw new ValidationException("CitaId debe ser un GUID válido.");

        if (dto.FechaAtencion == default)
            throw new ValidationException("FechaAtencion es obligatoria.");

        if (string.IsNullOrWhiteSpace(dto.MotivoConsulta))
            throw new ValidationException("MotivoConsulta es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.Sintomatologia))
            throw new ValidationException("Sintomatologia es obligatoria.");

        // Diagnostico puede estar vacío solo si EsPreAyudaDiagnostica
        if (string.IsNullOrWhiteSpace(dto.Diagnostico) && !dto.EsPreAyudaDiagnostica)
            throw new ValidationException("Diagnostico es obligatorio a menos que sea una atención previa a ayuda diagnóstica.");
    }

    public static void ValidateBusinessRules(RegistrarAtencionDto dto, IEnumerable<AtencionHistoriaClinica> existingAtenciones)
    {
        // No debe haber dos atenciones con la misma FechaAtencion
        if (existingAtenciones.Any(a => a.FechaAtencion == dto.FechaAtencion))
            throw new BusinessException("Ya existe una atención registrada para esta fecha.");
    }
}