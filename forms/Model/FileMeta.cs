namespace forms.Model;

public class FileMeta
{
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public long? FileSize { get; set; }
    public string? Base64Content { get; set; }
    public DateTime? UploadedAt { get; set; }
}