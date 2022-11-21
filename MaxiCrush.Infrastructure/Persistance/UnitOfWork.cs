using MaxiCrush.Application.Common.Interfaces.Persistance;
namespace MaxiCrush.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly MaxiCrushDbContext _context;

    public UnitOfWork(MaxiCrushDbContext context)
        => _context = context;

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
