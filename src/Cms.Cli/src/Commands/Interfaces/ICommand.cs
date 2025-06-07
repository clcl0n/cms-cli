using System.Threading;
using System.Threading.Tasks;

namespace Cms.Cli.Commands.Interfaces;

public interface ICommand
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
