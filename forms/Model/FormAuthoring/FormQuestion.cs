using forms.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator(Required = true)]
[
    BsonKnownTypes(
        typeof(ShortTextQuestion),
        typeof(LongTextQuestion),
        typeof(FileUploadQuestion),
        typeof(DateQuestion),
        typeof(NumberQuestion),
        typeof(SelectQuestion)
    )
]
public class FormQuestion : BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    [BsonElement("type")]
    public QuestionType Type { get; set; }

    [BsonElement("questionText")] 
    public string? QuestionText { get; set; }

    [BsonElement("description")]
    [BsonIgnoreIfNull]
    public string? Description { get; set; }

    [BsonElement("placeholder")]
    [BsonIgnoreIfNull]
    public string? Placeholder { get; set; }

    [BsonElement("required")] 
    public bool Required { get; set; }

    [BsonElement("order")] 
    public int? Order { get; set; }
}