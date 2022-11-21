using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Infrastructure.Persistance.Repositories;

public class ConfirmationCodeRepository : Repository<ConfirmationCode>, IConfirmationCodeRepository
{
    public ConfirmationCodeRepository(MaxiCrushDbContext context) : base(context)
    {
    }

    public Task<ConfirmationCode?> GetByEmailAsync(string email)
    {
        var entity = GetAll().FirstOrDefault(x => x.Email == email);
        return Task.FromResult(entity);
    }
}
