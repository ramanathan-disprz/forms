using MongoDB.Bson.Serialization.Attributes;

namespace forms.Model.FormAuthoring;

public class FileMeta
{
    [BsonElement("fileName")]
    public string FileName { get; set; } = string.Empty;

    [BsonElement("fileUrl")]
    public string FileUrl { get; set; } = string.Empty;

    [BsonElement("fileSizeBytes")]
    public long FileSizeBytes { get; set; }

    [BsonElement("mimeType")]
    public string MimeType { get; set; } = string.Empty;

    [BsonElement("uploadedAt")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}