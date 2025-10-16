using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace forms.Model.FormSubmission;

[Table("form_submissions")]
public class FormSubmission : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(24)]
    [Column("form_id")]
    public string FormId { get; set; } = string.Empty;

    [Required] 
    [Column("user_id")] 
    public long UserId { get; set; }

    [Required] 
    [Column("submitted_at")] 
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public void GenerateId()
    {
        Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}