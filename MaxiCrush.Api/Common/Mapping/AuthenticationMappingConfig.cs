using Mapster;
using MaxiCrush.Application.Controls.Authentication.Commands.Register;
using MaxiCrush.Application.Controls.Authentication.Common;
using MaxiCrush.Application.Controls.Authentication.Queries.Login;
using MaxiCrush.Contracts.Authentication.Login;
using MaxiCrush.Contracts.Authentication.Register;
using MaxiCrush.Contracts.Dto;
using MaxiCrush.Contracts.Users;
using MaxiCrush.Domain.Entities;
using System.Text.RegularExpressions;

namespace MaxiCrush.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<RegisterRequest, RegisterCommand>()
            .MapToConstructor(true);

        config.ForType<LoginRequest, LoginQuery>()
            .MapToConstructor(true);

        config.NewConfig<AuthenticationResult, LoginResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>())
            .MapToConstructor(true);

        config.NewConfig<AuthenticationResult, RegisterResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.CreatedUser, src => src.User.Adapt<UserDto>())
            .MapToConstructor(true);
    }
}
