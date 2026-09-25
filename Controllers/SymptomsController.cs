using Microsoft.AspNetCore.Mvc;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SymptomsController : ControllerBase
{
    private readonly ISymptomService _symptomService;
    
    public SymptomsController(ISymptomService symptomService)
    {
        _symptomService = symptomService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Symptom>>> GetAll()
    {
        var symptoms = await _symptomService.GetAllSymptoms();
        return Ok(symptoms);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<Symptom>> GetByCode(string code)
    {
        var symptom = await _symptomService.GetByCodeAsync(code);
        if(symptom is null) return NotFound();
        return Ok(symptom);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Symptom symptom)
    {
        await _symptomService.CreateAsync(symptom);
        return CreatedAtAction(nameof(GetByCode), new { code = symptom.Code }, symptom);
    }
}
