using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using MaxiCrush.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace MaxiCrush.Infrastructure.Persistance.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MaxiCrushDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return GetAll().FirstOrDefault(x => x.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public override IQueryable<User> ConfigureInclude(DbSet<User> set)
    {
        return set.Include(x => x.Role)
                  .ThenInclude(x => x.Permissions);
    }
}
