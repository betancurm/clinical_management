using MedicalOrdersMicroservice.Exceptions;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice.Validations;

public static class OrdenMedicaValidator
{
    public static void Validate(OrdenMedica orden)
    {
        if (string.IsNullOrWhiteSpace(orden.NumeroOrden) || orden.NumeroOrden.Length > 6)
            throw new ValidationException("NumeroOrden debe tener máximo 6 dígitos y no estar vacío.");

        if (string.IsNullOrWhiteSpace(orden.CedulaPaciente))
            throw new ValidationException("CedulaPaciente es obligatoria.");

        if (string.IsNullOrWhiteSpace(orden.CedulaMedico) || orden.CedulaMedico.Length > 10)
            throw new ValidationException("CedulaMedico debe tener máximo 10 dígitos y no estar vacío.");

        if (orden.CitaId == Guid.Empty)
            throw new ValidationException("CitaId debe ser un GUID válido.");
    }

    public static void ValidateBusinessRules(OrdenMedica orden)
    {
        // Exclusividad de ayuda diagnóstica: si hay ayudas diagnósticas, no puede haber medicamentos ni procedimientos
        if (orden.AyudasDiagnosticas.Any())
        {
            if (orden.Medicamentos.Any() || orden.Procedimientos.Any())
                throw new BusinessException("Cuando se receta una ayuda diagnóstica no puede recetarse procedimiento ni medicamento ya que no se tiene certeza del diagnóstico.");
        }

        // Unicidad de NumeroItem en la orden
        var allItems = orden.Medicamentos.Select(m => m.NumeroItem)
            .Concat(orden.Procedimientos.Select(p => p.NumeroItem))
            .Concat(orden.AyudasDiagnosticas.Select(a => a.NumeroItem))
            .ToList();

        if (allItems.Distinct().Count() != allItems.Count)
            throw new ConflictException("Los NumeroItem deben ser únicos dentro de la orden.");
    }
}