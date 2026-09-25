using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotoEngine_back.Models;

public class Rule
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("ruleCode")]
    public string RuleCode { get; set; } = null!;          

    [BsonElement("conditions")]
    public List<string> Conditions { get; set; } = new();  

    [BsonElement("conclusionCode")]
    public string ConclusionCode { get; set; } = null!;    

    [BsonElement("conclusionText")]
    public string ConclusionText { get; set; } = null!;  
}