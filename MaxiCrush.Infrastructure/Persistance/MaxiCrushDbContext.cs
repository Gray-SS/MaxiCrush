using MaxiCrush.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaxiCrush.Infrastructure.Persistance;

public class MaxiCrushDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }

    public MaxiCrushDbContext() : base() { }
    public MaxiCrushDbContext(DbContextOptions<MaxiCrushDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaxiCrushDbContext).Assembly);
    }
}
