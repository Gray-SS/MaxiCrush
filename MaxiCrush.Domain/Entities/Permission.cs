using System.ComponentModel.DataAnnotations;

namespace MaxiCrush.Domain.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();
}
