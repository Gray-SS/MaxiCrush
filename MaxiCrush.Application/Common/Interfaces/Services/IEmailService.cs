using MaxiCrush.Domain.Mailing.Interfaces;

namespace MaxiCrush.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendAsync(IEmailMessage message);
    Task SendAsync(IEmailMessageBuilder messageBuilder);
}
