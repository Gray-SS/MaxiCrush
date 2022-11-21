using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
}
