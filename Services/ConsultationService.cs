using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;

namespace MotoEngine_back.Services;

public class ConsultationService : IConsultationService
{
    private const int MaxQuestions = 8;

    private static readonly (string A, string B)[] Exclusive =
    {
        ("s_spark_yes", "s_spark_no"),
        ("s_starter_no", "s_starter_yes"),
        ("s_smoke_black", "s_smoke_white"),
    };

    private static readonly (string A, string B)[] Complement =
    {
        ("s_spark_yes", "s_spark_no"),
    };

    private readonly ISymptomService _symptoms;
    private readonly IRuleService _rules;
    private readonly IDiagnosisService _diagnosis;

    public ConsultationService(ISymptomService symptoms, IRuleService rules, IDiagnosisService diagnosis)
    {
        _symptoms = symptoms;
        _rules = rules;
        _diagnosis = diagnosis;
    }

    public async Task<ConsultationResponse> NextAsync(ConsultationRequest request)
    {

        var symptomList = await _symptoms.GetAllSymptoms();
        var rules = await _rules.GetAllAsync();
        var symptomByCode = symptomList.GroupBy(s => s.Code).ToDictionary(g => g.Key, g => g.First());

        var known = BuildKnown(request.Answers);
        var asked = request.Answers.Select(a => a.Code).ToHashSet();   
        var yesCodes = known.Where(k => k.Value).Select(k => k.Key).ToList();
        var diagnosis = await _diagnosis.DiagnoseAsync(new DiagnosisRequest { SelectedSymptomCodes = yesCodes });
        var firedCodes = diagnosis.FiredRules.Select(f => f.RuleCode).ToHashSet();

        string Label(string c) => symptomByCode.TryGetValue(c, out var s) ? s.Label : c;
        foreach (var f in diagnosis.FiredRules)
        {
            var rule = rules.FirstOrDefault(r => r.RuleCode == f.RuleCode);
            if (rule != null) f.Because = rule.Conditions.Select(Label).ToList();
        }

        bool IsUnknown(string c) =>
            symptomByCode.ContainsKey(c) && !known.ContainsKey(c) && !asked.Contains(c);

        var alive = rules
            .Where(r => !firedCodes.Contains(r.RuleCode))
            .Where(r => !r.Conditions.Any(c => known.TryGetValue(c, out var v) && !v))
            .Where(r => !r.Conditions.Any(c => asked.Contains(c) && !known.ContainsKey(c)))
            .ToList();

        var scores = new Dictionary<string, double>();
        foreach (var r in alive)
        {
            var unknown = r.Conditions.Where(IsUnknown).ToList();
            foreach (var c in unknown)
            {
                if (request.Group != null && symptomByCode[c].Group != request.Group) continue;
                scores[c] = scores.GetValueOrDefault(c) + 1.0 / unknown.Count;
            }
        }

        var hypotheses = alive
            .Select(r => new { r, Matched = r.Conditions.Count(c => known.GetValueOrDefault(c)), Total = r.Conditions.Count })
            .Where(x => x.Matched > 0 && x.Matched < x.Total)
            .OrderByDescending(x => (double)x.Matched / x.Total)
            .Take(3)
            .Select(x => new Hypothesis
            {
                RuleCode = x.r.RuleCode, Text = x.r.ConclusionText, Matched = x.Matched, Total = x.Total
            })
            .ToList();

        var answered = request.Answers.Count;
        var finished = scores.Count == 0 || answered >= MaxQuestions;

        return new ConsultationResponse
        {
            Finished = finished,
            Question = finished ? null : symptomByCode[scores.OrderByDescending(s => s.Value).First().Key],
            Hypotheses = hypotheses,
            FiredRules = diagnosis.FiredRules,
            QuestionNumber = answered + 1
        };
    }

    private static Dictionary<string, bool> BuildKnown(List<AnswerItem> answers)
    {
        var known = new Dictionary<string, bool>();
        foreach (var a in answers.Where(a => a.Value.HasValue))
            known[a.Code] = a.Value!.Value;

        foreach (var (x, y) in Exclusive)
        {
            if (known.GetValueOrDefault(x)) known.TryAdd(y, false);
            if (known.GetValueOrDefault(y)) known.TryAdd(x, false);
        }
        foreach (var (x, y) in Complement)
        {
            if (known.TryGetValue(x, out var vx) && !vx) known.TryAdd(y, true);
            if (known.TryGetValue(y, out var vy) && !vy) known.TryAdd(x, true);
        }
        return known;
    }
}