using Miunie.Core.Entities;
using Miunie.Core.Entities.Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Miunie.Core.Discord
{
    public interface IDiscordConnection
    {
        bool UserIsMiunie(MiunieUser user);
        Task RunAsync(CancellationToken cancellationToken);
        string GetBotAvatarUrl();
        ConnectionState ConnectionState { get; }
        event EventHandler ConnectionChanged;
    }
}
