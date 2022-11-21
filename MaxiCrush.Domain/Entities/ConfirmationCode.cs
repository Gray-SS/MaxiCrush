using System.ComponentModel.DataAnnotations;

namespace MaxiCrush.Domain.Entities;

public class ConfirmationCode
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Value { get; set; }
}
