using System;
using Miunie.Core;
using Miunie.Discord.Logging;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Miunie.Discord
{
    public class MiunieDiscord : IMiunieDiscord
    {
        public bool IsRunning { get; private set; }
        public event EventHandler ConnectionChanged;

        private readonly IDiscord _discord;
        private readonly DiscordLogger _discordLogger;
        private readonly CommandServiceFactory _cmdServiceFactory;

        public MiunieDiscord(IDiscord discord, DiscordLogger discordLogger, CommandServiceFactory cmdServiceFactory)
        {
            _discord = discord;
            _discordLogger = discordLogger;
            _cmdServiceFactory = cmdServiceFactory;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _discord.Initialize();
            _discord.Client.DebugLogger.LogMessageReceived += _discordLogger.Log;
            _cmdServiceFactory.Create(_discord.Client);
            await _discord.Client.ConnectAsync();
            IsRunning = true;
            ConnectionChanged?.Invoke(this, EventArgs.Empty);

            try
            {
                await Task.Delay(-1, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                await _discord.Client.DisconnectAsync();
                IsRunning = false;
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
