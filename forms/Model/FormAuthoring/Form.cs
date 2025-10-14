using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using forms.Enum;

namespace forms.Model.FormAuthoring;

[BsonIgnoreExtraElements]
public class Form : BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonElement("title")] 
    public string? Title { get; set; } = string.Empty;

    [BsonElement("description")]
    [BsonIgnoreIfNull]
    public string? Description { get; set; }

    [BsonElement("publishedBy")] public long? PublishedBy { get; set; }

    [BsonElement("publishedDate")]
    [BsonIgnoreIfNull]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? PublishedDate { get; set; }

    [BsonElement("formStatus")]
    [BsonRepresentation(BsonType.String)]
    public FormStatus FormStatus { get; set; } = FormStatus.Draft;

    [BsonElement("formViewStatus")]
    [BsonRepresentation(BsonType.String)]
    public FormViewStatus FormViewStatus { get; set; } = FormViewStatus.Enabled;

    [BsonElement("questionLimit")] 
    public int? QuestionLimit { get; set; }

    [BsonElement("allowMultipleResponses")]
    public bool AllowMultipleResponses { get; set; } = false;

    [BsonElement("questions")]
    [BsonIgnoreIfNull]
    public List<FormQuestion>? Questions { get; set; }
}