using MaxiCrush.Application.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MaxiCrush.Application.Common.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureFromAssembly(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        var settings = assembly.ExportedTypes.Where(x => x.GetCustomAttribute<AppSettingsAttribute>() != null && !x.IsAbstract && !x.IsInterface)
                                             .Select(Activator.CreateInstance)
                                             .ToList();

        foreach(var item in settings)
        {
            var attribute = item.GetType()
                                .GetCustomAttribute<AppSettingsAttribute>();

            services.Add(new ServiceDescriptor(item.GetType(), item));

            var section = configuration.GetSection(attribute.Name);
            if (section == null)
                throw new Exception();

            section.Bind(item);
        }

        return services;
    }
}
