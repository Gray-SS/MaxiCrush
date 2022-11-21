namespace MaxiCrush.Domain.Mailing.Interfaces;

public interface IEmailMessageBuilder
{
    IEmailMessageBuilder From(string from);
    IEmailMessageBuilder To(string to);
    IEmailMessageBuilder To(params string[] to);
    IEmailMessageBuilder WithSubject(string subject);
    IEmailMessageBuilder WithBody(string body);

    IEmailMessage Build();
}
