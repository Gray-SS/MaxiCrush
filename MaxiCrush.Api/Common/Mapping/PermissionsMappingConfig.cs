using Mapster;
using MaxiCrush.Domain.Entities;
using MaxiCrush.Contracts.Users.Me;
using MaxiCrush.Application.Controls.Users.Commands.Update;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Application.Controls.Users.Commands.SetRole;
using MaxiCrush.Contracts.Permissions;
using MaxiCrush.Application.Controls.Permissions.Commands.Update;
using MaxiCrush.Application.Controls.Permissions.Commands.Create;

namespace MaxiCrush.Api.Common.Mapping;

public class PermissionsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreatePermissionRequest, CreatePermissionCommand>()
            .MapToConstructor(true);

        config.ForType<UpdatePermissionRequest, UpdatePermissionCommand>()
            .MapToConstructor(true);

    }
}
