namespace FRESHY.Authentication.Infrastructure.Settings;

public class JwtSettings
{
    public string SectionName = "JwtSettings";
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
    public int ExpiryDays { get; set; }
    public string? Audience { get; set; }
}