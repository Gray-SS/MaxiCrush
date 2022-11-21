using FluentResults;
using MaxiCrush.Application.Common.Errors;
using MaxiCrush.Application.Common.Interfaces.Persistance;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace MaxiCrush.Infrastructure.Persistance;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly MaxiCrushDbContext _context;

    public Repository(MaxiCrushDbContext context)
    {
        _context = context;
    }

    public virtual IQueryable<TEntity> ConfigureInclude(DbSet<TEntity> set)
        => set;

    public TEntity First(Predicate<TEntity> predicate, bool isTracked = true)
    {
        return GetAll(isTracked).First(x => predicate.Invoke(x));
    }

    public TEntity? FirstOrDefault(Predicate<TEntity> predicate, bool isTracked = true)
    {
        return GetAll(isTracked).FirstOrDefault(x => predicate.Invoke(x));
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> RemoveAsync(TEntity entity)
    {
        _context.Remove(entity);
        return Task.FromResult(entity);
    }

    public ICollection<TEntity> GetAll(bool isTracked = true)
    {
        var set = _context.Set<TEntity>();
        var query = this.ConfigureInclude(set);

        if (!isTracked)
            query = query.AsNoTracking();

        return query.ToList();
    }
}