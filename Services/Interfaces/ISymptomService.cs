using MotoEngine_back.Models;

namespace MotoEngine_back.Services.Interfaces;

public interface ISymptomService
{
    Task<List<Symptom>> GetAllSymptoms(); 
    Task<Symptom?> GetByCodeAsync(string code);
    Task CreateAsync(Symptom symptom);
}