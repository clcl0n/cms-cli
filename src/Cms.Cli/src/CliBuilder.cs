using System.Threading.Tasks;
using Cms.Cli.Extensions;
using Microsoft.Extensions.Hosting;

namespace Cms.Cli;

public static class CliBuilder
{
    public static HostApplicationBuilder CreateCliBuilder(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddCommandRunner();

        return builder;
    }

    public static async Task RunCliAsync(HostApplicationBuilder builder)
    {
        IHost host = builder.Build();

        await host.RunAsync();
    }
}
