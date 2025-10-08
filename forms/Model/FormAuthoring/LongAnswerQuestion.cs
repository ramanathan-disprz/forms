using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("long_answer")]
[BsonIgnoreExtraElements]
public class LongAnswerQuestion : BaseQuestion
{
    [BsonElement("maxLength")]
    [BsonIgnoreIfNull]
    public int? MaxLength { get; set; }
    
    [BsonElement("minLength")]
    [BsonIgnoreIfNull]
    public int? MinLength { get; set; }

    public LongAnswerQuestion()
    {
        Type = QuestionType.LongAnswer;
    }
    
}