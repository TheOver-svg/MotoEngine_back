namespace MotoEngine_back.Models;

public class DiagnosisRequest
{
    public List<string> SelectedSymptomsCode { get; set; } = new();

}

public class FiredRule
{
    public string RuleCode { get; set;} = null!;

    public string ConculusionCode { get; set; } = null!;

    public string ConculusionText { get; set; } = null!;

}

