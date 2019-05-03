using DSharpPlus.EventArgs;
using Miunie.Core;
using Miunie.Discord.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Miunie.Core.Logging;
using DSharpPlus.Entities;

namespace Miunie.Discord
{
    public class MiunieDiscord : IMiunieDiscord
    {
        public string GetBotAvatarUrl()
            => _discord.Client?.CurrentUser?.AvatarUrl;

        private ConnectionState _connectionState;
        public ConnectionState ConnectionState
        {
            get => _connectionState;
            private set
            {
                _connectionState = value;
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ConnectionChanged;

        private readonly IDiscord _discord;
        private readonly DiscordLogger _discordLogger;
        private readonly CommandServiceFactory _cmdServiceFactory;
        private readonly ILogWriter _logger;

        public MiunieDiscord(IDiscord discord, DiscordLogger discordLogger, CommandServiceFactory cmdServiceFactory, ILogWriter logger)
        {
            _discord = discord;
            _discordLogger = discordLogger;
            _cmdServiceFactory = cmdServiceFactory;
            _logger = logger;

            _connectionState = ConnectionState.DISCONNECTED; 
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            ConnectionState = ConnectionState.CONNECTING;
            _discord.Initialize();
            _discord.Client.DebugLogger.LogMessageReceived += _discordLogger.Log;
            _discord.Client.Ready += ClientOnReady;
            _cmdServiceFactory.Create(_discord.Client);

            try
            {
                await _discord.Client.ConnectAsync();
                await Task.Delay(-1, cancellationToken);
            }
            catch (Exception ex)
            {
                await _discord.Client.DisconnectAsync();
                _discord.DisposeOfClient();
                _logger.LogError(ex.ToString());
            }
            finally
            {
                ConnectionState = ConnectionState.DISCONNECTED;
            }
        }

        private Task ClientOnReady(ReadyEventArgs e)
        {
            _logger.Log("Client Ready");
#if DEBUG
            _discord.Client.UpdateStatusAsync(new DiscordActivity
            {
                ActivityType = ActivityType.ListeningTo,
                Name = "Herself being created."
            });
#endif
            ConnectionState = ConnectionState.CONNECTED;
            return Task.CompletedTask;
        }
    }
}
