namespace MaxiCrush.Application.Common.Interfaces.Persistance;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
