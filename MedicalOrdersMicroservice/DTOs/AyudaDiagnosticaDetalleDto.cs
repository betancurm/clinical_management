namespace MedicalOrdersMicroservice.DTOs;

public class AyudaDiagnosticaDetalleDto
{
    public int NumeroItem { get; set; }
    public string NombreAyudaDiagnostica { get; set; }
    public Guid IdAyudaDiagnostica { get; set; }
    public int Cantidad { get; set; }
    public bool RequiereEspecialista { get; set; }
    public Guid? IdTipoEspecialidad { get; set; }
}