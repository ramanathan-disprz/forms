using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

public class Option
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonElement("label")] public string? Label { get; set; } = string.Empty;

    [BsonElement("value")]
    [BsonIgnoreIfNull]
    public string? Value { get; set; }

    public Option()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}