using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HistoriaClinicaMicroservice.Models;

[BsonIgnoreExtraElements]
public class AtencionHistoriaClinica
{
    [BsonElement("fechaAtencion")]
    public DateTime FechaAtencion { get; set; }

    [BsonElement("cedulaMedico")]
    public string CedulaMedico { get; set; }

    [BsonElement("motivoConsulta")]
    public string MotivoConsulta { get; set; }

    [BsonElement("sintomatologia")]
    public string Sintomatologia { get; set; }

    [BsonElement("diagnostico")]
    public string? Diagnostico { get; set; }

    [BsonElement("citaId")]
    public Guid CitaId { get; set; }

    [BsonElement("numerosOrdenAsociadas")]
    public List<string> NumerosOrdenAsociadas { get; set; } = new();

    [BsonElement("notasAdicionales")]
    public string? NotasAdicionales { get; set; }

    [BsonElement("datosAdicionales")]
    public BsonDocument? DatosAdicionales { get; set; }

    [BsonElement("esPreAyudaDiagnostica")]
    public bool EsPreAyudaDiagnostica { get; set; } = false;
}