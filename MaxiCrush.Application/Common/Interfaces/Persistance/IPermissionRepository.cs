using MaxiCrush.Domain.Entities;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission?> GetByIdAsync(Guid id);
    Task<Permission?> GetByNameAsync(string name);
}
