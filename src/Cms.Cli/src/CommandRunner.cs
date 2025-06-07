using System;
using System.Threading;
using System.Threading.Tasks;
using Cms.Cli.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cms.Cli;

internal sealed class CommandRunner(
    IHostApplicationLifetime applicationLifetime,
    IServiceProvider serviceProvider
) : IHostedService
{
    private Task task = Task.CompletedTask;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        task = ExecuteCommandAsync(cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return task;
    }

    private async Task ExecuteCommandAsync(CancellationToken cancellationToken)
    {
        try
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var args = Environment.GetCommandLineArgs();

                var commandToExecute =
                    args.Length > 1
                        ? args[1]
                        : throw new InvalidOperationException("Command is required");

                ICommand command = scope.ServiceProvider.GetRequiredKeyedService<ICommand>(
                    commandToExecute
                );

                await command.ExecuteAsync(cancellationToken);
            }

            applicationLifetime.StopApplication();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing command: {ex.Message}");
            applicationLifetime.StopApplication();
        }
    }
}
