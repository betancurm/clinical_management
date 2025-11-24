using InventoryMicroservice.Exceptions;
using InventoryMicroservice.Models;

namespace InventoryMicroservice.Validations;

public static class MedicamentoValidator
{
    public static void Validate(Medicamento medicamento)
    {
        if (string.IsNullOrWhiteSpace(medicamento.Nombre))
            throw new ValidationException("El nombre del medicamento es obligatorio.");
    }
}

public static class MedicamentoLoteValidator
{
    public static void Validate(MedicamentoLote lote)
    {
        if (string.IsNullOrWhiteSpace(lote.Lote))
            throw new ValidationException("El lote es obligatorio.");

        if (lote.FechaFabricacion >= lote.FechaExpiracion)
            throw new ValidationException("La fecha de fabricación debe ser anterior a la fecha de expiración.");

        if (lote.FechaExpiracion <= DateTime.UtcNow)
            throw new ValidationException("La fecha de expiración debe ser futura.");

        if (lote.CantidadDisponible < 0)
            throw new ValidationException("La cantidad disponible no puede ser negativa.");
    }
}