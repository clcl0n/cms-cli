using Microsoft.Extensions.DependencyInjection;

namespace Cms.Cli.Extensions;

internal static class SetupCommandRunnerExtension
{
    public static IServiceCollection AddCommandRunner(this IServiceCollection services)
    {
        services.AddHostedService<CommandRunner>();

        return services;
    }
}
