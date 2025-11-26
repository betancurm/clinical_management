using MongoDB.Driver;
using HistoriaClinicaMicroservice.Models;

namespace HistoriaClinicaMicroservice.Configurations;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString"));
        _database = client.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
    }

    public IMongoCollection<HistoriaClinicaDocumento> HistoriaClinica => _database.GetCollection<HistoriaClinicaDocumento>("HistoriaClinica");
}