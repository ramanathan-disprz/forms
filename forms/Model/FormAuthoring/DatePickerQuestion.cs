using MongoDB.Bson.Serialization.Attributes;
using forms.Enum;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("date_picker")]
[BsonIgnoreExtraElements]
public class DatePickerQuestion : BaseQuestion
{
    [BsonElement("minDate")]
    [BsonIgnoreIfNull]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? MinDate { get; set; }

    [BsonElement("maxDate")]
    [BsonIgnoreIfNull]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? MaxDate { get; set; }

    // Display format for clients (e.g., "DD/MM/YYYY" or "MM/DD/YYYY")
    [BsonElement("format")]
    [BsonIgnoreIfNull]
    public string? Format { get; set; }

    public DatePickerQuestion()
    {
        Type = QuestionType.DatePicker;
    }
}

/*
Store dates as UTC DateTime to keep them unambiguous across timezones.
Convert from ISO-8601 strings to DateTimeOffset on input, then assign.
UtcDateTime to MinDateUtc/MaxDateUtc.
*/