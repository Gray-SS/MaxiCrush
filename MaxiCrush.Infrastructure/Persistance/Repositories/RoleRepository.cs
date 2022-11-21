using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace MaxiCrush.Infrastructure.Persistance.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(MaxiCrushDbContext context) : base(context)
    {
    }

    public override IQueryable<Role> ConfigureInclude(DbSet<Role> set)
    {
        return set.Include(x => x.Users)
                  .Include(x => x.Permissions);
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return GetAll().FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
    }
}
