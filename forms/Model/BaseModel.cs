using System.ComponentModel.DataAnnotations.Schema;

namespace forms.Model;

public abstract class BaseModel
{
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")] 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // TODO : Add logic to handle update
}