using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace forms.Model.FormSubmission;

[Table("form_answers")]
public class FormAnswer : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }
    
    [Required]
    [Column("submission_id")]
    public long SubmissionId { get; set; }
    
    [Required]
    [Column("question_id")]
    public string QuestionId { get; set; } = string.Empty;  
    
    [Required]
    [Column("question_type")]
    public string QuestionType { get; set; } = string.Empty;
    
    // For short, long, numeric, datepicker
    [Column("value_text")]
    public string? ValueText { get; set; }

    // For dropdown, file upload
    [Column("value_json", TypeName = "json")]
    public string? ValueJson { get; set; }
    
    public void GenerateId()
    {
        Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}
