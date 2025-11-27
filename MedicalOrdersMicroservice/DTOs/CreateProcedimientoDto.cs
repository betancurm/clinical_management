namespace MedicalOrdersMicroservice.DTOs;

public class CreateProcedimientoDto
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int DuracionEstimadaMinutos { get; set; }
    public decimal Costo { get; set; }
    public bool Activo { get; set; } = true;
}