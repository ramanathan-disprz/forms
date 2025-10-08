using System.ComponentModel.DataAnnotations.Schema;

namespace forms.Model;

[Table("admins")]
public class Admin : BaseModel
{
    [Column("form_count")]
    public int FormLimit { get; set; }
    
}