using FluentResults;

namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity First(Predicate<TEntity> predicate, bool isTracked = true);
    TEntity? FirstOrDefault(Predicate<TEntity> predicate, bool isTracked = true);

    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> RemoveAsync(TEntity entity);

    ICollection<TEntity> GetAll(bool isTracked = true);
}
