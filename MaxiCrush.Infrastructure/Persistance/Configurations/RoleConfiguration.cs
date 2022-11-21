using MaxiCrush.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaxiCrush.Infrastructure.Persistance.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Users).WithOne(x => x.Role);
    }
}
