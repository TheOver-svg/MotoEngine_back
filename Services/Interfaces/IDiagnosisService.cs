using MotoEngine_back.Models;

namespace MotoEngine_back.Services.Interfaces;

public interface IDiagnosisService
{
    Task<DiagnosisResult> DiagnoseAsync(DiagnosisRequest request);
}