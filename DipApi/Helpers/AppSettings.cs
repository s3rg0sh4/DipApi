namespace DipApi.Helpers;

public class AppSettings
{
    public string Secret { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string TokenValidityInMinutes { get; set; }
    public string RefreshTokenValidityInDays { get; set; }
}