using FluentResults;
using FluentValidation;
using MaxiCrush.Application.Controls.Authentication.Commands.Register;
using MaxiCrush.Application.Controls.Authentication.Common;
using MaxiCrush.Application.Common.Extensions;
using MaxiCrush.Application.Common.Validation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaxiCrush.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>),
                           typeof(ValidationBehavior<,>));

        services.ConfigureFromAssembly(configuration, typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
