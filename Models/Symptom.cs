using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotoDiagnostics.Api.Models;

public class Symptom
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    [BsonElement("code")]
    public string Code {get;set;} = null!;
    [BsonElement("label")]
    public string Label {get;set;} = null!;
    [BsonElement("group")]
    public string Group { get; set; } = null!;

}