namespace MedicalOrdersMicroservice.DTOs;

public class ProcedimientoDetalleDto
{
    public int NumeroItem { get; set; }
    public string NombreProcedimiento { get; set; }
    public Guid? IdProcedimiento { get; set; }
    public int NumeroVeces { get; set; }
    public string Frecuencia { get; set; }
    public decimal? Costo { get; set; }
    public bool RequiereEspecialista { get; set; }
    public Guid? IdTipoEspecialidad { get; set; }
}