using MongoDB.Bson.Serialization.Attributes;
using forms.Enum;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("select")]
[BsonIgnoreExtraElements]
public class SelectQuestion : FormQuestion
{
    [BsonElement("options")]
    public List<Option> Options { get; set; } = new();

    [BsonElement("multiSelect")]
    [BsonIgnoreIfNull]
    public bool? MultiSelect { get; set; } // true if multiple selection allowed

    public SelectQuestion()
    {
        Type = QuestionType.Select;
    }

    // Optional helper: ensure Option.Value defaults to Option.Label before saving
    public void NormalizeOptions()
    {
        foreach (var o in Options)
        {
            if (string.IsNullOrWhiteSpace(o.Value))
                o.Value = o.Label;
        }
    }
}