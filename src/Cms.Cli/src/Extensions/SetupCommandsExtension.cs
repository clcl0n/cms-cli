using Cms.Cli.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.Cli.Extensions;

public static class SetupCommandsExtension
{
    public static IServiceCollection AddCommand<TCommand>(
        this IServiceCollection services,
        string commandName
    )
        where TCommand : class, ICommand
    {
        services.AddKeyedScoped<ICommand, TCommand>(commandName);

        return services;
    }
}
