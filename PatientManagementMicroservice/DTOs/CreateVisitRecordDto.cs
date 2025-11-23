using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice.DTOs;

public class CreateVisitRecordDto
{
    public int? PresionArterialSistolica { get; set; }
    public int? PresionArterialDiastolica { get; set; }
    public decimal? Temperatura { get; set; }
    public int? Pulso { get; set; }
    public int? Oxigenacion { get; set; }
    public string? Notas { get; set; }
}