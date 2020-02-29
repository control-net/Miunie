using Miunie.Core;
using Miunie.Discord.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Miunie.Core.Logging;
using Discord;

namespace Miunie.Discord
{
    public class MiunieDiscord : IMiunieDiscord
    {
        public bool UserIsMiunie(MiunieUser user)
            => user.UserId == _discord.Client?.CurrentUser?.Id;
        public string GetBotAvatarUrl()
            => _discord.Client?.CurrentUser?.GetAvatarUrl();

        private Core.ConnectionState _connectionState;
        public Core.ConnectionState ConnectionState
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
        private readonly ILogWriter _logger;
        private readonly CommandHandler _commandHandler;

        public MiunieDiscord(IDiscord discord, DiscordLogger discordLogger, ILogWriter logger, CommandHandler commandHandler)
        {
            _discord = discord;
            _discordLogger = discordLogger;
            _logger = logger;
            _commandHandler = commandHandler;

            _connectionState = Core.ConnectionState.DISCONNECTED; 
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            ConnectionState = Core.ConnectionState.CONNECTING;

            try
            {
                await _discord.InitializeAsync();
                _discord.Client.Log += _discordLogger.Log;
                _discord.Client.Ready += ClientOnReady;
                await _commandHandler.InitializeAsync();
                await _discord.Client.StartAsync();
                await Task.Delay(-1, cancellationToken);
            }
            catch (Exception ex)
            {
                if (_discord.Client != null)
                {
                    await _discord.Client.LogoutAsync();
                    _discord.DisposeOfClient();
                }

                _logger.LogError(ex.Message);
            }
            finally
            {
                ConnectionState = Core.ConnectionState.DISCONNECTED;
            }
        }

        private Task ClientOnReady()
        {
            _logger.Log("Client Ready");
#if DEBUG
            _discord.Client.SetGameAsync("Herself being created.", type: ActivityType.Watching);
#endif
            ConnectionState = Core.ConnectionState.CONNECTED;
            return Task.CompletedTask;
        }
    }
}
