namespace MaxiCrush.Contracts.Roles;

public record UpdateRoleRequest(
   string? Name = null,
   int? Power = null,
   Guid[]? PermissionIds = null);
