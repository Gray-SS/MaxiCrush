using MaxiCrush.Domain.Mailing.Interfaces;

namespace MaxiCrush.Infrastructure.Mailing;

public class EmailMessageBuilder : IEmailMessageBuilder
{
    private string _from;
    private string[] _to;
    private string _body;
    private string _subject;

    public IEmailMessageBuilder From(string from)
    {
        _from = from;
        return this;
    }

    public IEmailMessageBuilder To(string to)
    {
        _to = new[] { to };
        return this;
    }

    public IEmailMessageBuilder To(params string[] to)
    {
        _to = to;
        return this;
    }

    public IEmailMessageBuilder WithBody(string body)
    {
        _body = body;
        return this;
    }

    public IEmailMessageBuilder WithSubject(string subject)
    {
        _subject = subject;
        return this;
    }

    public IEmailMessage Build()
    {
        return new EmailMessage(_from, _to, _subject, _body);
    }
}
