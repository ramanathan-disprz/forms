using forms.Enum;

namespace forms.Dto;

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }
}