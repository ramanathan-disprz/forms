namespace forms.Request;

public class FileUploadRequest : BaseQuestionRequest
{
    public string[]? AllowedFileTypes { get; set; }
    public long? MaxFileSizeMB { get; set; }
    public long? MaxTotalFileSizeMB { get; set; }
    public int? MaxFiles { get; set; }
}