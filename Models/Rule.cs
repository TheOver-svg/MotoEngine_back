using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotoEngine_back.Models;

public class Rule
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("ruleCode")]
    public string RuleCode { get; set; } = null!;          // напр. "R1"

    [BsonElement("conditions")]
    public List<string> Conditions { get; set; } = new();  // напр. ["s_starter_no", "s_battery_low"]

    [BsonElement("conclusionCode")]
    public string ConclusionCode { get; set; } = null!;     // напр. "c1"

    [BsonElement("conclusionText")]
    public string ConclusionText { get; set; } = null!;  
}