using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Services;

public class DiagnosisService : IDiagnosisService
{
    private readonly IRuleService _ruleService;

    public DiagnosisService(IRuleService ruleService)
    {
        _ruleService = ruleService;
    }

    public async Task<DiagnosisResult> DiagnoseAsync(DiagnosisRequest request)
    {
        var rules = await _ruleService.GetAllAsync();
        var facts = new HashSet<string>(request.SelectedSymptomCodes);
        var fired = new List<Rule>();

        bool changed = true;
        while (changed)
        {
            changed = false;
            foreach (var rule in rules)
            {
                if (fired.Contains(rule)) continue;
                if (rule.Conditions.All(c => facts.Contains(c)))
                {
                    facts.Add(rule.ConclusionCode);
                    fired.Add(rule);
                    changed = true;
                }
            }
        }

        return new DiagnosisResult
        {
            FiredRules = fired.Select(r => new FiredRule
            {
                RuleCode = r.RuleCode,
                ConclusionCode = r.ConclusionCode,
                ConclusionText = r.ConclusionText
            }).ToList()
        };
    }
}