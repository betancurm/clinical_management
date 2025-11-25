namespace MedicalOrdersMicroservice.DTOs;

public class MedicamentoDetalleDto
{
    public int NumeroItem { get; set; }
    public string NombreMedicamento { get; set; }
    public Guid? IdMedicamento { get; set; }
    public string Dosis { get; set; }
    public string DuracionTratamiento { get; set; }
    public decimal? Costo { get; set; }
}