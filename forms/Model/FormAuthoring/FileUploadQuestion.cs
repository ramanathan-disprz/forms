using forms.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

[BsonDiscriminator("file_upload")]
[BsonIgnoreExtraElements]
public class FileUploadQuestion : BaseQuestion
{
    [BsonElement("allowedFileTypes")]
    [BsonIgnoreIfNull]
    public string[]? AllowedFileTypes { get; set; }

    [BsonElement("maxFileSizeMB")]
    [BsonIgnoreIfNull]
    public int? MaxFileSizeMB { get; set; }

    [BsonElement("maxTotalFileSizeMB")]
    [BsonIgnoreIfNull]
    public int? MaxTotalFileSizeMB { get; set; }

    [BsonElement("maxFiles")]
    [BsonIgnoreIfNull]
    public int? MaxFiles { get; set; }

    [BsonElement("storagePath")]
    [BsonIgnoreIfNull]
    public string? StoragePath { get; set; }

    [BsonElement("uploadedFiles")]
    [BsonIgnoreIfNull]
    public List<FileMeta>? UploadedFiles { get; set; }

    public FileUploadQuestion()
    {
        Type = QuestionType.FileUpload;
    }
}