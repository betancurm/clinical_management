namespace MedicalOrdersMicroservice.DTOs;

public class BatchOrdenesDto
{
    public IEnumerable<string> NumeroOrdenes { get; set; } = new List<string>();
}