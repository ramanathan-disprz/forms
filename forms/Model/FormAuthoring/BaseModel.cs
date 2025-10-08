using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonIgnoreExtraElements]
public abstract class BaseModel
{
    [BsonElement("created_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updated_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

// TODO : Add logic to handle update
}