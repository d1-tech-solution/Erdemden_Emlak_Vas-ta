namespace BussinessLayer.Settings;

public class GoogleAnalyticsSettings
{
    public const string SectionName = "GoogleAnalyticsSettings";
    public string PropertyId { get; set; } = string.Empty;
    public string CredentialsFilePath { get; set; } = string.Empty;
}
