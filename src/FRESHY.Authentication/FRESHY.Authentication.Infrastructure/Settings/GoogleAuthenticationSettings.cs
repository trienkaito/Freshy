namespace FRESHY.Authentication.Infrastructure.Settings;

public class GoogleAuthenticationSettings
{
    public string SectionName = "GoogleOAuth";
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}