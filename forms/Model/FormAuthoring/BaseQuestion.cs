using forms.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator(Required = true)]
[
    BsonKnownTypes(
        typeof(ShortAnswerQuestion),
        typeof(LongAnswerQuestion),
        typeof(FileUploadQuestion),
        typeof(DatePickerQuestion),
        typeof(NumericQuestion),
        typeof(DropdownQuestion)
    )
]
public abstract class BaseQuestion : BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonElement("type")]
    public QuestionType Type { get; set; }

    [BsonElement("questionText")] 
    public string QuestionText { get; set; } = string.Empty;

    [BsonElement("description")]
    [BsonIgnoreIfNull]
    public string? Description { get; set; }

    [BsonElement("placeholder")]
    [BsonIgnoreIfNull]
    public string? Placeholder { get; set; }

    [BsonElement("required")] 
    public bool Required { get; set; }

    [BsonElement("order")] 
    public int Order { get; set; }

    // string | string[] | null
    [BsonElement("defaultValue")]
    [BsonIgnoreIfNull]
    public object? DefaultValue { get; set; }

    // string | string[] | null
    [BsonElement("answer")]
    [BsonIgnoreIfNull]
    public object? Answer { get; set; }
}