namespace MaxiCrush.Contracts.Dto;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Power { get; set; }

    public IEnumerable<PermissionDto> Permissions { get; set; }
}
