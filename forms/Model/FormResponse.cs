using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace forms.Model;

[Table("form_responses")]
public class FormResponse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }

    // Mongo Form _id as string (24-hex).
    [Required]
    [MaxLength(24)]
    [Column("form_id")]
    public string FormId { get; set; } = string.Empty;

    [Required] 
    [Column("user_id")] 
    public long UserId { get; set; }

    [Required] 
    [Column("submitted_at")] 
    public DateTime SubmittedAt { get; set; }

    [Required]
    [Column("answers", TypeName = "json")]
    public string Answers { get; set; } = "{}";

    [NotMapped]
    public JsonDocument AnswersJson =>
        string.IsNullOrWhiteSpace(Answers)
            ? JsonDocument.Parse("{}")
            : JsonDocument.Parse(Answers);

    public void GenerateId()
    {
        Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}