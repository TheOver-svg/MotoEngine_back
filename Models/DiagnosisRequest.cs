namespace MotoEngine_back.Models;

public class DiagnosisRequest
{
    public List<string> SelectedSymptomCodes { get; set; } = new();

}

public class FiredRule
{
    public string RuleCode { get; set;} = null!;

    public string ConclusionCode { get; set; } = null!;

    public string ConclusionText { get; set; } = null!;

    public List<string> Because { get; set; } = new();

}

