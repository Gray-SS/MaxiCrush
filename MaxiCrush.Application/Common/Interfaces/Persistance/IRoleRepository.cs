using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string name);
}
