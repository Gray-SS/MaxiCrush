using System.ComponentModel.DataAnnotations;

namespace MaxiCrush.Domain.Entities;

public class ConfirmationToken
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationDate { get; set; }
}
