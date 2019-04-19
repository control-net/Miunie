using System;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core
{
    public interface IMiunieDiscord
    {
        Task RunAsync(CancellationToken cancellationToken);
        string GetBotAvatarUrl();
        ConnectionState ConnectionState { get; }
        event EventHandler ConnectionChanged;
    }
}
