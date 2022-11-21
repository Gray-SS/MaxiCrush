using Mapster;
using MaxiCrush.Application.Controls.Roles.Commands.Create;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Roles;
using MaxiCrush.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MaxiCrush.Api.Common.Mapping;

public class RolesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Role, RoleDto>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Permissions, src => src.Permissions.Select(x => x.Adapt<PermissionDto>()));

        config.ForType<CreateRoleRequest, CreateRoleCommand>()
            .MapToConstructor(true);
    }
}
