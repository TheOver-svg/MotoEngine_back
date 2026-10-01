using Microsoft.AspNetCore.Mvc;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationController : ControllerBase
{
    private readonly IConsultationService _service;

    public ConsultationController(IConsultationService service) => _service = service;

    [HttpPost("next")]
    public async Task<ActionResult<ConsultationResponse>> Next(ConsultationRequest request) =>
        Ok(await _service.NextAsync(request));
}