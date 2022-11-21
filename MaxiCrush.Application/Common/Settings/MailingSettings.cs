namespace MaxiCrush.Application.Common.Settings;

[AppSettings("Mailgun")]
public class MailingSettings
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string Domain { get; set; }
}
