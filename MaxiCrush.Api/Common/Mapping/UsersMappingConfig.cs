using Mapster;
using MaxiCrush.Domain.Entities;
using MaxiCrush.Contracts.Users.Me;
using MaxiCrush.Application.Controls.Users.Commands.Update;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Application.Controls.Users.Commands.SetRole;
using MaxiCrush.Contracts.Dto;

namespace MaxiCrush.Api.Common.Mapping;

public class UsersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<(UpdateMeRequest Request, Guid Id), UpdateUserCommand>()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest, src => src.Request)
            .MapToConstructor(true);

        config.NewConfig<User, UserDto>()
            .Map(dest => dest.Gender, src => Enum.Parse<Gender>(src.Gender))
            .Map(dest => dest.GenderInterestedIn, src => Enum.Parse<Gender>(src.GenderInterestedIn))
            .Map(dest => dest.Role, src => src.Role.Adapt<RoleDto>());
    }
}
