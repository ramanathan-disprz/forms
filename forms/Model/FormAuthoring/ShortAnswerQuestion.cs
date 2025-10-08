using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("short_answer")]
[BsonIgnoreExtraElements]
public class ShortAnswerQuestion : BaseQuestion
{
    [BsonElement("maxLength")]
    [BsonIgnoreIfNull]
    public int? MaxLength { get; set; }

    public ShortAnswerQuestion()
    {
        Type = QuestionType.ShortAnswer;
    }
}