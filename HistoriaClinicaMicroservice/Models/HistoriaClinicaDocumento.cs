using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HistoriaClinicaMicroservice.Models;

[BsonIgnoreExtraElements]
public class HistoriaClinicaDocumento
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("cedulaPaciente")]
    public string CedulaPaciente { get; set; }

    [BsonElement("pacienteId")]
    public Guid? PacienteId { get; set; }

    [BsonElement("atenciones")]
    public List<AtencionHistoriaClinica> Atenciones { get; set; } = new();
}