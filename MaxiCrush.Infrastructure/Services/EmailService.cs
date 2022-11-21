using RestSharp.Authenticators;
using RestSharp;
using MaxiCrush.Application.Common.Settings;
using MaxiCrush.Application.Common.Interfaces.Services;
using MaxiCrush.Domain.Mailing.Interfaces;

namespace MaxiCrush.Infrastructure.Services;

internal class EmailService : IEmailService
{
    private MailingSettings _mailingSettings;

    public EmailService(MailingSettings mailingSettings)
    {
        _mailingSettings = mailingSettings;
    }

    public Task SendAsync(IEmailMessage message)
    {
        var client = new RestClient("https://api.mailgun.net/v3")
        {
            Authenticator = new HttpBasicAuthenticator("api", _mailingSettings.ApiKey)
        };

        var request = new RestRequest();
        request.AddParameter("domain", _mailingSettings.Domain, ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from", $"{message.From} <mailgun@{_mailingSettings.Domain}>");

        for (int i = 0; i < message.To.Length; i++)
        {
            request.AddParameter("to", message.To[i]);
        }

        request.AddParameter("subject", message.Subject);
        request.AddParameter("text", message.Body);
        request.Method = Method.Post;

        client.Execute(request);
        return Task.CompletedTask;
    }

    public async Task SendAsync(IEmailMessageBuilder messageBuilder)
        => await SendAsync(messageBuilder.Build());
}
