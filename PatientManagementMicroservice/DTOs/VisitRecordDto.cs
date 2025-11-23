using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.DTOs;

public class VisitRecordDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime FechaVisita { get; set; }
    public int? PresionArterialSistolica { get; set; }
    public int? PresionArterialDiastolica { get; set; }
    public decimal? Temperatura { get; set; }
    public int? Pulso { get; set; }
    public int? Oxigenacion { get; set; }
    public string? Notas { get; set; }
}