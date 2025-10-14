using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

public class Option
{
    [BsonElement("id")]
    public string Id { get; set; } = string.Empty; 

    [BsonElement("label")]
    public string? Label { get; set; } = string.Empty; 

    [BsonElement("value")]
    [BsonIgnoreIfNull]
    public string? Value { get; set; } 
}