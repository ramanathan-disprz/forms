namespace forms.Dto;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public long ExpiresAt { get; set; }
}