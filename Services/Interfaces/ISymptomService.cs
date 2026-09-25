using MotoDiagnostics.Api.Models;

namespace MotoDiagnostics.Api.Services.Interfaces;

public interface ISymptomService
{
    Task<List<Symptom>> GetAllAsync(); 
}