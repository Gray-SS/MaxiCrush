using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaxiCrush.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null;
    public string Lastname { get; set; } = null;
    public string Email { get; set; } = null;
    public string Gender { get; set; }
    public string GenderInterestedIn { get; set; }
    public string Biography { get; set; } = null;
    public byte[] Password { get; set; } = null;
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }

    [NotMapped]
    public bool IsBanned => DateTime.UtcNow < EndBanDate;
    public string? BanReason { get; set; }
    public DateTime EndBanDate { get; set; }

    public Role Role { get; set; }
}
