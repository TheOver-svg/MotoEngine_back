using Microsoft.AspNetCore.Mvc;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosisController : ControllerBase
{
    private readonly IDiagnosisService _diagnosisService;

    public DiagnosisController(IDiagnosisService diagnosisService)
    {
        _diagnosisService = diagnosisService;
    }

    [HttpPost]
    public async Task<ActionResult<DiagnosisResult>> Diagnose(DiagnosisRequest request)
    {
        var result = await _diagnosisService.DiagnoseAsync(request);
        return Ok(result);
    }
}