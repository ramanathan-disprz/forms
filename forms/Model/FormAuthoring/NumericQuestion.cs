using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("numeric")]
[BsonIgnoreExtraElements]
public class NumericQuestion : BaseQuestion
{
    [BsonElement("maxValue")]
    [BsonIgnoreIfNull]
    public int? MaxValue { get; set; }
    
    [BsonElement("minLength")]
    [BsonIgnoreIfNull]
    public int? MinValue { get; set; }
    
    public NumericQuestion()
    {
        Type = QuestionType.Numeric;
    }
    
}