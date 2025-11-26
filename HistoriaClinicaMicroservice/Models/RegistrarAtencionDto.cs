namespace HistoriaClinicaMicroservice.Models;

public class RegistrarAtencionDto
{
    public string CedulaPaciente { get; set; }
    public string CedulaMedico { get; set; }
    public Guid CitaId { get; set; }
    public DateTime FechaAtencion { get; set; }
    public string MotivoConsulta { get; set; }
    public string Sintomatologia { get; set; }
    public string? Diagnostico { get; set; }
    public List<string> NumerosOrdenAsociadas { get; set; } = new();
    public string? NotasAdicionales { get; set; }
    public Dictionary<string, object>? DatosAdicionales { get; set; }
    public bool EsPreAyudaDiagnostica { get; set; } = false;
}