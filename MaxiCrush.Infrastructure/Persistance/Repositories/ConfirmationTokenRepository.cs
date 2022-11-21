using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Infrastructure.Persistance.Repositories;

public class ConfirmationTokenRepository : Repository<ConfirmationToken>, IConfirmationTokenRepository
{
    public ConfirmationTokenRepository(MaxiCrushDbContext context) : base(context)
    {
    }

    public Task<ConfirmationToken?> GetByEmailAsync(string email)
    {
        return Task.FromResult(GetAll().FirstOrDefault(x => x.Email == email));
    }
}
