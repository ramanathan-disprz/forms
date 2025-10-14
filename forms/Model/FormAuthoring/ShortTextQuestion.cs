using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("short_text")]
[BsonIgnoreExtraElements]
public class ShortTextQuestion : FormQuestion
{
    [BsonElement("maxLength")]
    [BsonIgnoreIfNull]
    public int? MaxLength { get; set; }

    public ShortTextQuestion()
    {
        Type = QuestionType.ShortText;
    }
}