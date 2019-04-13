using System;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IMiunieDiscord
    {
        Task RunAsync(CancellationToken cancellationToken);
        bool IsRunning { get; }
        event EventHandler ConnectionChanged;
    }
}
