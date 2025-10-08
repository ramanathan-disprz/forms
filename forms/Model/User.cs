using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using forms.Enum;

namespace forms.Model;

[Table("users")]
public class User : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [Column("email")]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [Column("password")]
    [MaxLength(512)]
    public required string? Password { get; set; }
    
    [Required]
    [Column("user_role")]
    [MaxLength(50)]
    public UserRole UserRole { get; set; } = UserRole.Learner;

    public void GenerateId()
    {
        Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}