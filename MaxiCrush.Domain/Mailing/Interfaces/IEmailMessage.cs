namespace MaxiCrush.Domain.Mailing.Interfaces;

public interface IEmailMessage
{
    string From { get; set; }
    string[] To { get; set; }
    string Subject { get; set; }
    string Body { get; set; }
}
