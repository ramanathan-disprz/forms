using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace forms.Model;

[Table("admins")]
public class Admin : BaseModel
{
    [Key]
    [Column("user_id")] 
    public long UserId { get; set; }

    [Column("form_count")] 
    public int FormLimit { get; set; }
}