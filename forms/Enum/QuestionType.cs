using MongoDB.Bson.Serialization.Attributes;

namespace forms.Enum;

public enum QuestionType
{
    ShortAnswer,
    LongAnswer,
    FileUpload,
    DatePicker,
    Numeric,
    Dropdown
}