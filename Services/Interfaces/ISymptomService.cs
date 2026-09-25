using MotoDiagnostics.Api.Models;

namespace MotoDiagnostics.Api.Services.Interfaces;

public interface ISymptomService
{
    Task<List<Symptom>> GetAllSymptoms(); 
    Task<Symptom?> GetByCodeAsync(string code);
    Task CreateAsync(Symptom symptom);
}