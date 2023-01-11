using System;
using GestaoEducacional.CC.Ioc;

namespace GestaoEducacional.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDIConfiguration(this IServiceCollection services)
    {
        NativeInjectorBootStrapper.RegisterServices(services);
    }
}