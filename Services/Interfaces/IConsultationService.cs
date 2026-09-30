using MotoEngine_back.Models;

namespace MotoEngine_back.Services.Interfaces;

public interface IConsultationService
{
    Task<ConsultationResponse> NextAsync(ConsultationRequest request);
}