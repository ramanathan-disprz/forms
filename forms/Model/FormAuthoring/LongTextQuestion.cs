using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("long_text")]
[BsonIgnoreExtraElements]
public class LongTextQuestion : FormQuestion
{
    [BsonElement("maxLength")]
    [BsonIgnoreIfNull]
    public int? MaxLength { get; set; }
    
    [BsonElement("minLength")]
    [BsonIgnoreIfNull]
    public int? MinLength { get; set; }

    public LongTextQuestion()
    {
        Type = QuestionType.LongText;
    }
    
}