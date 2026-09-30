namespace MotoEngine_back.Models;

public class AnswerItem
{
    public string Code { get; set; } = null!;
    public bool? Value { get; set; }          
}

public class ConsultationRequest
{
    public string? Group { get; set; }
    public List<AnswerItem> Answers { get; set; } = new();
}

public class Hypothesis
{
    public string RuleCode { get; set; } = null!;
    public string Text { get; set; } = null!;
    public int Matched { get; set; }
    public int Total { get; set; }
}

public class ConsultationResponse
{
    public bool Finished { get; set; }
    public Symptom? Question { get; set; }
    public List<Hypothesis> Hypotheses { get; set; } = new();
    public List<FiredRule> FiredRules { get; set; } = new();
    public int QuestionNumber { get; set; }
}