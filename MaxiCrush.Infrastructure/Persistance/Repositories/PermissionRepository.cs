using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaxiCrush.Infrastructure.Persistance.Repositories;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(MaxiCrushDbContext context) : base(context)
    {
    }

    public override IQueryable<Permission> ConfigureInclude(DbSet<Permission> set)
    {
        return set.Include(x => x.Roles);
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public async Task<Permission?> GetByNameAsync(string name)
    {
        return GetAll().FirstOrDefault(x => x.Name == name);
    }
}
