using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IConfirmationTokenRepository : IRepository<ConfirmationToken>
{
    Task<ConfirmationToken?> GetByEmailAsync(string email);
}
