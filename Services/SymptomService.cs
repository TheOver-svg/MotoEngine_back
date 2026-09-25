using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;
using MotoEngine_back.Settings;

namespace MotoEngine_back.Services;

public class SymptomService : ISymptomService
{
    private readonly IMongoCollection<Symptom> _symptoms;

    public SymptomService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _symptoms = database.GetCollection<Symptom>(settings.Value.SymptomsCollectionName); 
    }

    public async Task<List<Symptom>> GetAllSymptoms()
    {
        return await _symptoms.Find(_ => true).ToListAsync();
    }

    public async Task<Symptom?> GetByCodeAsync(string code)
    {
        return await _symptoms.Find(s => s.Code == code).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Symptom symptom)
    {
        await _symptoms.InsertOneAsync(symptom);
    }
}