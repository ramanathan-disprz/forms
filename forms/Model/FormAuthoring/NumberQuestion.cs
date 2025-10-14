using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("number")]
[BsonIgnoreExtraElements]
public class NumberQuestion : FormQuestion
{
    [BsonElement("maxValue")]
    [BsonIgnoreIfNull]
    public int? MaxValue { get; set; }
    
    [BsonElement("minLength")]
    [BsonIgnoreIfNull]
    public int? MinValue { get; set; }
    
    public NumberQuestion()
    {
        Type = QuestionType.Number;
    }
    
}