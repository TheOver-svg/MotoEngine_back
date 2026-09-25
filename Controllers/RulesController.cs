using Microsoft.AspNetCore.Mvc;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly IRuleService _ruleService;

    public RulesController(IRuleService ruleService)
    {
        _ruleService = ruleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rule>>> GetAll()
    {
        var rules = await _ruleService.GetAllAsync();
        return Ok(rules);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Rule rule)
    {
        await _ruleService.CreateAsync(rule);
        return Ok(rule);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _ruleService.DeleteAsync(id);
        return NoContent();
    }
}