using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IConfirmationCodeRepository : IRepository<ConfirmationCode>
{
    Task<ConfirmationCode?> GetByEmailAsync(string email);
}
