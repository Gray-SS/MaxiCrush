using System.ComponentModel.DataAnnotations;

namespace MaxiCrush.Domain.Entities;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Power { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}