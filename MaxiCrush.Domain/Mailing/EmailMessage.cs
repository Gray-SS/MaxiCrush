using MaxiCrush.Domain.Mailing.Interfaces;

namespace MaxiCrush.Infrastructure.Mailing;

public class EmailMessage : IEmailMessage
{
    public string From { get; set; }
    public string[] To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailMessage(string from, string[] to, string subject, string body)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
    }
}